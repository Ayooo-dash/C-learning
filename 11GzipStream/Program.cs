using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11GzipStream
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args[0] == "-c")
                {
                    Console.WriteLine(GZip(args[1]));
                }
                else if (args[0] == "-d")
                {
                    Console.WriteLine(UnGZip(args[1]));
                }
                else if (args[0] == "-h")
                {
                    Console.WriteLine("帮助：");
                    Console.WriteLine("     -c：压缩文件");
                    Console.WriteLine("     -d：解压文件");
                    Console.WriteLine("     -h：查看帮助");
                }
                else
                {
                    Console.Write("unknown command:\"{0}\"！", args[0]);
                }
            }
            catch
            {
                Console.WriteLine("输入{0} -h查看帮助！", Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            }
        }

        public static bool GZip(string strPath)
        {
            try
            {
                using (FileStream fsRead = File.OpenRead(strPath))
                {
                    using (FileStream fsWrite = File.OpenWrite(strPath + ".gz"))
                    {
                        using (GZipStream gzipStream = new GZipStream(fsWrite, CompressionMode.Compress))
                        {
                            byte[] buf = new byte[1024];
                            int r = 0;
                            while ((r = fsRead.Read(buf, 0, buf.Length)) > 0)
                            {
                                gzipStream.Write(buf, 0, r);
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool UnGZip(string strPath)
        {
            try
            {
                using (FileStream fsRead = File.OpenRead(strPath))
                {
                    using (GZipStream gzipStream = new GZipStream(fsRead, CompressionMode.Decompress))
                    {
                        using (FileStream fsWrite = File.OpenWrite(strPath.Remove(strPath.Length - 3, 3)))
                        {
                            byte[] buf = new byte[1024];
                            int r = 0;
                            while ((r = gzipStream.Read(buf, 0, buf.Length)) > 0)
                            {
                                fsWrite.Write(buf, 0, r);
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Zip(string filesPath, string zipFilePath)
        {
            if (!Directory.Exists(filesPath))
            {
                Console.WriteLine("Cannot fine director '{0}'", filesPath);
                return false;
            }
            try
            {
                string[] filenames = Directory.GetFiles(filesPath);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    s.SetLevel(9);                   //压缩级别 0-9
                    // s.Password = "123";           //Zip压缩文件密码
                    byte[] buffer = new byte[4096];  //缓冲区大小
                    foreach (string file in filenames)
                    {
                        if (file == filenames[filenames.Length - 1] || file == filenames[filenames.Length - 2] || file == filenames[filenames.Length - 3])
                        {
                            ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                            entry.DateTime = DateTime.Now;
                            s.PutNextEntry(entry);
                            using (FileStream fs = File.OpenRead(file))
                            {
                                int sourceBytes;
                                do
                                {
                                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                    s.Write(buffer, 0, sourceBytes);
                                } while (sourceBytes > 0);
                            }
                        }
                    }
                    s.Finish();
                    s.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during processing {0}", ex);
                return false;
            }
        }
    }
}
