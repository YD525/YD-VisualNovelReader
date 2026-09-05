using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace YDVNR.UIManagement
{
    public class RenderThread
    {
        private object ArrayLock = new object();
        private List<ImageBlock> WaitRender = new List<ImageBlock>();

        public Thread RenderTrd = null;
        private bool RenderService = false;
        private object RenderLock = new object();
        public void StartService(bool Check)
        {
            
            lock (RenderLock)
            {
                if (Check && !RenderService)
                {
                    RenderService = true;

                    if (RenderTrd != null)
                    {
                        RenderTrd = new Thread(() =>
                        {
                            while (RenderService)
                            {
                                Thread.Sleep(10);
                                lock (ArrayLock)
                                {
                                    if (WaitRender.Count > 0)
                                    {
                                        var GetFirst = WaitRender[0];

                                        //

                                        WaitRender.RemoveAt(0);
                                    }

                                }
                            }
                        });
                    }
                }
                else
                {
                    RenderService = false;
                    try 
                    { 
                        if (RenderTrd != null)
                        {
                            RenderTrd.Abort();
                        }
                    }
                    catch { }
                }
            }
        }

        public void Put(ImageBlock Block)
        {
            lock (ArrayLock)
            {
                if (!WaitRender.Contains(Block))
                {
                    WaitRender.Insert(0, Block);
                }
            }
        }
        public void Cancel(ImageBlock Block)
        {
            lock (ArrayLock)
            {
                if (WaitRender.Contains(Block))
                {
                    WaitRender.Remove(Block);
                }
            }
        }
    }
    public class ImageBlock
    {
        public string Path = "";
        public Grid ParentRef;

        public BitmapSource Source;
        public RenderThread TrdRef;

        public void Render()
        {
            TrdRef.Put(this);
        }
        public void Release()
        {
            TrdRef.Cancel(this);
        }
    }
    public class BookView : IDisposable
    {
        public RenderThread Thread = null;
        public List<ImageBlock> Rows = new List<ImageBlock>();
        public BookView()
        {
            if (Thread == null)
            {
                Thread = new RenderThread();
                Thread.StartService(true);
            }
        }

        public void Add()
        { 
        
        }

        public void Remove()
        { 
        
        }

        public void ScrollTo()
        { 
        
        }

        public void Dispose()
        {
            if (Thread != null)
            {
                Thread.StartService(false);
            }
        }
    }
}
