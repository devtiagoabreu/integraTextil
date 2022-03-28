using System.Text;
using System.ComponentModel;
using System.Data;
using Ionic.Zip;
using System.Net.Mail;
using System.Net;
using DAL;
using DAO;


namespace BLL
{
    public class BLLFerramentas
    {
        public List<string> PegarNomesArquivo(string pasta, string extensaoNomeArquivo)
        {
            List<string> arquivoNome = new List<string>();

            // Take a snapshot of the file system.  
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(pasta);

            // This method assumes that the application has discovery permissions  
            // for all folders under the specified path.  
            IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            //Create the query  
            IEnumerable<System.IO.FileInfo> fileQuery =
                from file in fileList
                where file.Extension == extensaoNomeArquivo //".csv"
                //where file.Name.Contains(parteNomeArquivo)
                orderby file.Name
                select file;

            //Execute the query. This might write out a lot of files!  
            foreach (System.IO.FileInfo fi in fileQuery)
            {
                arquivoNome.Add(fi.FullName);
            }

            return arquivoNome;
        }

        public void CriarArquivoZip(List<string> arquivos, string ArquivoDestino)
        {
            using (ZipFile zip = new ZipFile())
            {
                // percorre todos os arquivos da lista
                foreach (string item in arquivos)
                {
                    // se o item é um arquivo
                    if (File.Exists(item))
                    {
                        try
                        {
                            // Adiciona o arquivo na pasta raiz dentro do arquivo zip
                            zip.AddFile(item, "");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                    // se o item é uma pasta
                    else if (Directory.Exists(item))
                    {
                        try
                        {
                            // Adiciona a pasta no arquivo zip com o nome da pasta 
                            zip.AddDirectory(item, new DirectoryInfo(item).Name);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                // Salva o arquivo zip para o destino
                try
                {
                    zip.Save(ArquivoDestino);
                }
                catch
                {
                    throw;
                }
            }
        }

        public void ExtrairArquivoZip(string localizacaoArquivoZip, string destino)
        {
            if (File.Exists(localizacaoArquivoZip))
            {
                //recebe a localização do arquivo zip
                using (ZipFile zip = new ZipFile(localizacaoArquivoZip))
                {
                    //verifica se o destino existe
                    if (Directory.Exists(destino))
                    {
                        try
                        {
                            //extrai o arquivo zip para o destino
                            zip.ExtractAll(destino);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                    else
                    {
                        //lança uma exceção se o destino não existe
                        throw new DirectoryNotFoundException("O arquivo destino não foi localizado");
                    }
                }
            }
            else
            {
                //lança uma exceção se a origem não existe
                throw new FileNotFoundException("O Arquivo Zip não foi localizado");
            }
        }

        public void EnviarEmail(string assunto, string corpoEmail, string de, string para, string cc, string anexo)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.Subject = "integraTêxtil | " + assunto;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = "Log de execução coleta de dados referente a --> " + corpoEmail + " \n" + "Atenciosamente, " + " \n" + "AtriosTech Solutions";
                    mailMessage.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                    mailMessage.BodyEncoding = Encoding.GetEncoding("UTF-8");
                    mailMessage.From = new MailAddress(de); //"atriostechsolutions@gmail.com"
                    mailMessage.To.Add(para);
                    mailMessage.CC.Add(cc);
                    mailMessage.Attachments.Add(new Attachment(anexo)); //@"D:\"

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("atriostechsolutions@gmail.com", "d87hbkx7x9QWERT$");

                    smtpClient.EnableSsl = true;

                    smtpClient.Send(mailMessage);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Nao foi Possivel enviar e-mails. Detalhes: " + ex.Message);
            }
          
        }

        public void GravarLog(string CaminhoArquivoLog, string log) 
        {
            // This text is added only once to the file.
            if (!File.Exists(CaminhoArquivoLog))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(CaminhoArquivoLog))
                {
                    sw.WriteLine(log);
                    sw.Close();
                }
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(CaminhoArquivoLog))
            {
                sw.WriteLine(log);
                sw.Close();
            }

            //C:\Apache2\htdocs\integratextil\teares\logs
            //StreamWriter sw = new StreamWriter(CaminhoArquivoLog);  
            //sw.WriteLine(log);
            //sw.Close();
        }

    }   
}
