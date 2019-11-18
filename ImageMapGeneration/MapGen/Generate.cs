using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using SimplexNoise;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using static System.Threading.Tasks.Parallel;

namespace MapGen
{

    public class Generate
    {

        public MemoryStream DrawMap(int width,int height,float modify = 0.24f , float Zoomv = 1f)
            {
                float[,] Map = SimplexNoise.Noise.Calc2D(width, height,(width / ((width * height * Zoomv ) * 1/15f)) * modify );
                float distance_x = MathF.Abs((width/2f) -(width * height * Zoomv ) * 0.5f);
                float distance_y = MathF.Abs((height/2f) - (width * height * Zoomv ) * 0.5f);
                float distance = MathF.Sqrt(distance_x*distance_x + distance_y*distance_y); // circular mask

                float max_width = (width * height * Zoomv ) * 0.5f - 10.0f;
                float delta = distance / max_width;
                float gradient = delta * delta;


                using var image = new Image<Rgba32>(width, height);
                image.Mutate(x => x.Fill(Rgba32.White));

                    Parallel.For(0,image.Width, (i) =>
                    {
                        Parallel.For(0,image.Height, (j) =>
                        {
                            var f = (Map[i,j] * 1/255f * gradient) ;
                            if(f < 0.1)
                                image[i,j] = Rgba32.White;
                            else if(f  < 0.19 )
                                image[i,j] = Rgba32.DarkGray;
                            else                             if(f < 0.55f )
                                image[i,j] = Rgba32.Green;
                            else                             if(f < 0.65 )
                                image[i,j] = Rgba32.LightGoldenrodYellow;
                            else if (f < 0.77)
                            {
                                image[i,j] = Rgba32.LightBlue;

                            }
                            else
                            {
                                image[i,j] = Rgba32.Blue;
                                
                            }
                        });

                    });
                    image.Save("imagen.png");
                    MemoryStream stream = new MemoryStream();
                    image.Save(stream, PngFormat.Instance);
                    stream.Position = 0;
                    return stream;
            }
        }
    }