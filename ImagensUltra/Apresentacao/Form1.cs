using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Drawing.Imaging;


namespace ImagensUltra
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int id2;
        public byte[] imagem;  //primeira imagem
        public byte[] imagem2; //segunda imagem
        public byte[] imagem3; //terceira imagem
        
        Conexao con = new Conexao();
        

            private void btnVerificar_Click(object sender, EventArgs e)
            {
            

            id2 = Convert.ToInt32(txtId.Text);  
            
            int id3 = id2 +1;                   
            int id1 = id2 - 1;                  

            lblContadorImagemId.Text = Convert.ToString(id2);
            SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;

                cmd.CommandText = @"select foto from imagem where id = @id1 or id = @id2 or id = @id3";
                                                                     
                cmd.Parameters.AddWithValue("@id1", id1);
                cmd.Parameters.AddWithValue("@id2", id2);
                cmd.Parameters.AddWithValue("@id3", id3);

            //verificar o banco de dados
            cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
    
                if (dr.HasRows)
                {
                   
                try
                {

                    dr.Read();
                    imagem = (byte[])dr["foto"];   //id1
                }
                catch (Exception)
                {

                    MessageBox.Show("não tem imagens no item 1");
                }
                try
                {

             
                dr.Read(); 
                    imagem2 = (byte[])dr["foto"];   //id2
                }
                catch (Exception)
                {

                    MessageBox.Show("não tem imagens no item 2");
                }
                try
                {
                    dr.Read();
                    imagem3 = (byte[])dr["foto"];   //id3
                }
                catch (Exception)
                {
                    MessageBox.Show("não tem imagens no item 3");
                }              
            }
            else
            {
                id1 = 0;
                id2 = 0;
                id3 = 0;
            }
            try
            {
                //converte os bytes em imagem
                txtImagemGrande.Image = debyteParaImagem(imagem2);
                txtImagem1.Image = debyteParaImagem(imagem);
                txtImagem2.Image = debyteParaImagem(imagem2);
                txtImagem3.Image = debyteParaImagem(imagem3);

                lblTamanho.Text = Convert.ToString(imagem2.Length);
            }
            catch (Exception)
            {


            }
            try
            {

            lblTamanho.Text = Convert.ToString(imagem2.Length);
            }
            catch (Exception)
            {

              
            }
            try
                {

                //fechar o datareader
                dr.Close();
                //encerrar a conexão
                con.Desconectar();

                }
                catch (Exception)
                {

                    MessageBox.Show("erro ao encerrar a aplicação");
                }

            }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) //acessa o item
            {
                FileInfo file = new FileInfo(openFileDialog1.FileName);  //
                txtImagemGrande.ImageLocation = openFileDialog1.FileName;  //
                lblTamanho.Text = Convert.ToString(file.Length);  //verifica o tamanho do arquivo
            }
  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            id2 = Convert.ToInt32(txtId.Text);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"delete from imagem where id= @id";

            cmd.Parameters.AddWithValue("@id", this.id2);

            try
            {    
            cmd.Connection = con.Conectar();
            cmd.ExecuteNonQuery();
           
              
                MessageBox.Show("Id deletado com sucesso", "Parabéns!");// corpo e titulo
            }
            catch (Exception)
            {

                MessageBox.Show("erro de banco de dados","Error");
            }
            con.Desconectar();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            imagem2 = imagemParaByte(txtImagemGrande.Image);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"insert into imagem values(foto = @foto)";

        
            cmd.Parameters.AddWithValue("@foto", imagem2);

            try
            {
                cmd.Connection = con.Conectar();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item cadastrado com sucesso");
            }
            catch (Exception)
            {

                MessageBox.Show("Erro de comunicação com o banco de dados");
            }
            finally
            {
                con.Desconectar();
            }

        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            id2 = Convert.ToInt16(txtId.Text);
            imagem2 = imagemParaByte(txtImagemGrande.Image);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"update imagem set foto = @imagem where id = @id";
            

            cmd.Parameters.AddWithValue("@id", id2);
            cmd.Parameters.AddWithValue("@imagem", imagem);

            try
            {

           
            cmd.Connection = con.Conectar();
            cmd.ExecuteNonQuery();
                MessageBox.Show("Item atualizado com sucesso");
            }
            catch (Exception)
            {

                MessageBox.Show("Erro de comunicação com o banco de dados");
            }
            finally
            {
                con.Desconectar();
            }
        }

        private void txtImagem2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
           id2 = Convert.ToInt16(lblContadorImagemId.Text);

            id2 += 1;
            int id3 = id2 + 1;                   
            int id1 = id2 - 1;

            lblContadorImagemId.Text = Convert.ToString(id2);

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = @"select foto from imagem where id = @id1 or id = @id2 or id = @id3";


            cmd.Parameters.AddWithValue("@id1", id1);
            cmd.Parameters.AddWithValue("@id2", id2);
            cmd.Parameters.AddWithValue("@id3", id3);
            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
               
            }
            catch (Exception)
            {

                throw;
            }
           
                if (dr.HasRows)
                {
                    try
                    {

                        dr.Read();
                        imagem = (byte[])dr["foto"];   //id1
                    }
                    catch (Exception)
                    {
                        
                    }
                    try
                    {
                        dr.Read();
                        imagem2 = (byte[])dr["foto"];   //id2
                    }
                    catch (Exception)
                    {
                      
                    }
                    try
                    {
                        dr.Read();
                        imagem3 = (byte[])dr["foto"];   //id3
                    }
                    catch (Exception)
                    {
                        
                    }

                }
                else
                {
               
                    MessageBox.Show("não tem mais imagens no banco de dados!");
                }
            try
            {

          
                txtImagemGrande.Image = debyteParaImagem(imagem2);
                txtImagem1.Image = debyteParaImagem(imagem);
                txtImagem2.Image = debyteParaImagem(imagem2);
                txtImagem3.Image = debyteParaImagem(imagem3);

                lblTamanho.Text = Convert.ToString(imagem2.Length);

            }
            catch (Exception)
            {

                
            }


            dr.Close();
            con.Desconectar();

            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            id2 = 0;
            id2 = Convert.ToInt16(lblContadorImagemId.Text);
            
            id2 -= 1;
            if (id2 > -1) 
            {

           
            int id3 = id2 + 1;
            int id1 = id2 - 1;

            lblContadorImagemId.Text = Convert.ToString(id2);

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = @"select foto from imagem where id = @id1 or id = @id2 or id = @id3";


            cmd.Parameters.AddWithValue("@id1", id1);
            cmd.Parameters.AddWithValue("@id2", id2);
            cmd.Parameters.AddWithValue("@id3", id3);
           
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();

          

            if (dr.HasRows)
            {
                try
                {

                    dr.Read();
                    imagem = (byte[])dr["foto"];   //id1
                }
                catch (Exception)
                {
                    
                }
                try
                {
                    dr.Read();
                    imagem2 = (byte[])dr["foto"];   //id2
                }
                catch (Exception)
                {
                   
                }
                try
                {
                    dr.Read();
                    imagem3 = (byte[])dr["foto"];   //id3
                }
                catch (Exception)
                {
                    
                }

            }
            else
            {
                MessageBox.Show("não tem mais imagens no banco de dados!");
            }
                try
                {

             
            txtImagemGrande.Image = debyteParaImagem(imagem2);
            txtImagem1.Image = debyteParaImagem(imagem);
            txtImagem2.Image = debyteParaImagem(imagem2);
            txtImagem3.Image = debyteParaImagem(imagem3);

            lblTamanho.Text = Convert.ToString(imagem2.Length);

                }
                catch (Exception)
                {

                    
                }


                dr.Close();
            con.Desconectar();
            }
            else
            {
                MessageBox.Show("id não pode ser menor que 0");
            }


        }
        //convert o byte para imagem
        public Image debyteParaImagem(byte[] byteArrayIn)
        {

            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;

        }

        //convert imagem em byte
        public byte[] imagemParaByte(System.Drawing.Image imageIn)
        {


            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

    }
    }



