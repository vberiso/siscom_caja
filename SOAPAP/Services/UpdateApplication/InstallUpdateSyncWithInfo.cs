using SOAPAP.Enums;
using SOAPAP.UI;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.Services.UpdateApplication
{
    public class InstallUpdateSyncWithInfo
    {
        public static void InstallUpdateSyncWithInfoApplication()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    Form mensaje = new MessageBoxForm("Error", "La nueva versión de la aplicación no se puede descargar en este momento."+Environment.NewLine+"Por favor, compruebe su conexión de red o inténtelo de nuevo más tarde.Error: " + dde.Message , TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    Form mensaje = new MessageBoxForm("Error", "No se puede buscar una nueva versión de la aplicación. La implementación de ClickOnce está dañada. Vuelva a desplegar la aplicación y vuelva a intentarlo. Error: " + ide.Message, TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    Form mensaje = new MessageBoxForm("Error", "Esta aplicación no puede ser actualizada. Es probable que no sea una aplicación ClickOnce. Error: " + ioe.Message, TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        Form mensaje = new MessageBoxForm("Actualización disponible", "Hay disponible una actualización. ¿Desea actualizar la aplicación ahora?", TypeIcon.Icon.Info);
                        if (!(DialogResult.OK == mensaje.ShowDialog()))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        Form mensaje = new MessageBoxForm("Actualización disponible", "Esta aplicación ha detectado una actualización obligatoria de su actual " +
                                                                                      "versión a versión " + info.MinimumRequiredVersion.ToString() +
                                                                                      ". La aplicación ahora instalará la actualización y se reiniciará.", TypeIcon.Icon.Warning);
                        mensaje.ShowDialog();
                      
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            ad.Update();
                            Form mensaje = new MessageBoxForm(Variables.titleprincipal, "La aplicación se ha actualizado y ahora se reiniciará. ", TypeIcon.Icon.Warning);
                            mensaje.ShowDialog();
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            Form mensaje = new MessageBoxForm(Variables.titleprincipal, "No se puede instalar la última versión de la aplicación. " + Environment.NewLine + "Por favor, compruebe su conexión de red o inténtelo de nuevo más tarde. Error: " +dde, TypeIcon.Icon.Warning);
                            mensaje.ShowDialog();
                            return;
                        }
                    }
                }
            }
        }
    }
}
