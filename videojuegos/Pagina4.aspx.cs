using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace videojuegos
{
    public partial class Pagina4 : System.Web.UI.Page
    {
        protected OdbcConnection conectarBD()
        {
            String stringConexion = "Driver={SQL Server Native Client 11.0};Server=cc102-11\\SA;Uid=sa;Pwd=adminadmin;Database=GameSpot";
            try
            {
                OdbcConnection conexion = new OdbcConnection(stringConexion);
                conexion.Open();
                lbError.Text = "conexion exitosa";
                return conexion;
            }
            catch (Exception ex)
            {
                lbError.Text = ex.StackTrace.ToString();
                return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OdbcConnection miConexion = conectarBD();
                if (miConexion != null)
                {
                    String query = String.Format("select juegos.nombre from juegos, escriben where escriben.claveU={0} and escriben.claveJ= juegos.claveJ", Session["claveUnica"].ToString());
                    OdbcCommand cmd = new OdbcCommand(query, miConexion);
                    OdbcDataReader rd = cmd.ExecuteReader();
                    ddJuegos.Items.Clear();
                    //Cada read pasa a la siguiente fila; no necesitas hacer una nueva variable de DataReader
                    while (rd.Read())
                    {
                        ddJuegos.Items.Add(rd.GetString(0));
                    }
                    rd.Close();

                }
            }
        }

        protected void ddJuegos_SelectedIndexChanged(object sender, EventArgs e)
        {
            OdbcConnection miConexion = conectarBD();
            if (miConexion != null)
            {
                String query = String.Format("select claveJ from juegos where nombre='{0}'", ddJuegos.SelectedValue);
                OdbcCommand cmd = new OdbcCommand(query, miConexion);
                OdbcDataReader rd = cmd.ExecuteReader();
                rd.Read();
                int claveJuego = rd.GetInt16(0);
                String query2 = String.Format("select nombre, resumen, consola, fechaLanzamiento from juegos where ClaveJ={0}", claveJuego);
                OdbcCommand cmd2 = new OdbcCommand(query2, miConexion);
                OdbcDataReader rd2 = cmd2.ExecuteReader();
                gvJuegos.DataSource = rd2;
                gvJuegos.DataBind();
                rd.Close();
                rd2.Close();
            }
        }

        protected void gvJuegos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}