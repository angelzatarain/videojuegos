using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace videojuegos
{
    public partial class Pagina1 : System.Web.UI.Page
    {
        protected OdbcConnection conectarBD()
        {
            String stringConexion = "Driver={SQL Server Native Client 11.0};Server=cc102-11\\SA;Uid=sa;Pwd=adminadmin;Database=GameSpot";
            try
            {
                OdbcConnection conexion = new OdbcConnection(stringConexion);
                conexion.Open();
                lbContador.Text = "conexion exitosa";
                return conexion;
            }
            catch (Exception ex)
            {
                lbContador.Text = ex.StackTrace.ToString();
                return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btPagina2_Click(object sender, EventArgs e)
        {
            OdbcConnection miConexion = conectarBD();
            if (miConexion != null) {
                String query = "select claveU from usuario where email='" + txUsuario.Text + "' and Password='" + txContraseña.Text + "'";
                OdbcCommand cmd = new OdbcCommand(query, miConexion);
                OdbcDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows){
                    rd.Read();
                    //objeto sescion para pasar la clave Unica de la pagina 1 a la 2
                    Session["claveUnica"] = rd.GetInt32(0).ToString();
                    Response.Redirect("Pagina2.aspx");
                    //Si un query tiene varias columbnas y varios renglones, cada DataReader es para un renglón, y adentró del GetString, GetInt, etc va el número de la colúmna;
                    //es decir, si un query tiene dos columnas y dos renglones y quieres el dato del segundo renglon y la segunda columna debes hacer dos datareaders y poner getInt(1)
                    rd.Close();
                }
                else {
                    lbContador.Text = "usuario o contraseña incorrectos";
                }
            }
        }
    }
}