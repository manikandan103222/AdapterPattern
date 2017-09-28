using System;

namespace AdapterPattern
{
    class Program
    {
        //'ITarget' Interface
        interface IVideoPlayer
        {
            void PlayVideo(string videoType, string fileName);
        }

        //'Adaptee' Interface which is act as additional feature interface
        interface IAdditionalVideoPlayer
        {
            void PlayFlv(string fileName);
            void PlayMpg(string fileName);
        }

        //'Adaptee' class 1
        public class FlvPlayer : IAdditionalVideoPlayer
        {
            public void PlayFlv(string fileName)
            {
                Console.WriteLine("Playing flv File. Name: " + fileName);
            }

            public void PlayMpg(string fileName)
            {
                //do nothing
            }
        }

        //'Adaptee' class 2
        public class MpgPlayer : IAdditionalVideoPlayer
        {
            public void PlayFlv(string fileName)
            {
                //do nothing
            }

            public void PlayMpg(string fileName)
            {
                Console.WriteLine("Playing mpg File. Name: " + fileName);
            }
        }

        //'Adapter' Class
        class VideoAdapter : IVideoPlayer
        {
            IAdditionalVideoPlayer additionalVideoPlayer;

            public VideoAdapter(string videoType)
            {
                if (videoType.Contains("flv"))
                {
                    additionalVideoPlayer = new FlvPlayer();
                } else if (videoType.Contains("mpg"))
                {
                    additionalVideoPlayer = new MpgPlayer();
                }
            }

            public void PlayVideo(string videoType, string fileName)
            {
                if (videoType.Contains("flv"))
                {
                    additionalVideoPlayer.PlayFlv(fileName);
                } else if (videoType.Contains("mpg"))
                {
                    additionalVideoPlayer.PlayMpg(fileName);
                }
            }
        }

        //'Target' class implementing the ITarget interface
        class VideoPlayer : IVideoPlayer
        {
            VideoAdapter videoAdapter;

            public void PlayVideo(string videoType, string fileName)
            {
                //inbuild support video files format
                if (videoType.Contains("avi") || videoType.Contains("wmv"))
                {
                    Console.WriteLine(string.Format("Playing {0} file. Name: {1}", videoType, fileName));
                }

                //videoAdapter is providing support to play other file formats
                else if (videoType.Contains("flv") || videoType.Contains("mpg"))
                {
                    videoAdapter = new VideoAdapter(videoType);
                    videoAdapter.PlayVideo(videoType, fileName);
                }

                else
                {
                    Console.WriteLine("Invalid Video " + videoType + "file format not supported");
                }
            }
        }

        //'Client'
        static void Main(string[] args)
        {
            VideoPlayer videoPlayer = new VideoPlayer();

            Console.WriteLine("------------------------------------Adapter Pattern for Video Player-----------------------------\n");
            videoPlayer.PlayVideo("avi", "bahubali-1.avi");
            videoPlayer.PlayVideo("wmv", "bahubali-2.wmv");
            videoPlayer.PlayVideo("flv", "wonder woman.flv");
            videoPlayer.PlayVideo("mpg", "spider man.mpg");

            Console.Write("Press any key to exist...");
            Console.ReadKey();
        }
    }
}
