using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class collider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //araclara ya da yanlara deðince oyunu bitirir
        if (other.gameObject.CompareTag("zemin"))
        {
            trafficracer.Kopya.seskutusu.PlayOneShot(trafficracer.Kopya.kazasesi);//kaza sesini çalar
            //trafficracer.Kopya.skor = 0;//skoru sýfýrlar
            trafficracer.Kopya.baslatici = false;//oyunu durdurur
            //trafficracer.Kopya.baslabutton.gameObject.SetActive(true);//basla butonunu gösterir
            trafficracer.Kopya.oyunbitti.text = "OYUN BÝTTÝ ";//ekrana oyun bitti yazdýrýr
            
            foreach (GameObject klon in trafficracer.Kopya.aktifAraclar)
            {
                foreach (GameObject item in trafficracer.Kopya.araclar)
                {
                    if (item != klon)
                    {
                        Destroy(klon);//araclarý yok eder
                        Debug.Log("sfkmlds");
                    }
                }
            }
            trafficracer.Kopya.aktifAraclar.Clear();//aktifaraclar listesini temizler
           //trafficracer.Kopya.aracýmýz.transform.position = new Vector3(0, 0, -11);//aracýmýzý ortaya getirir
            trafficracer.Kopya.fiziksistemi.ResetInertiaTensor();//aracýmýzýn üzerindeki kuvvetleri sýfýrlar
            trafficracer.Kopya.fiziksistemi.velocity = Vector3.zero;//ayný þekilde
            trafficracer.Kopya.fiziksistemi.angularVelocity = Vector3.zero;//ayný 
            trafficracer.Kopya.yenidenbaslaButonu.gameObject.SetActive(true);//yeniden baþlama butonunu gösterir
        }

        //her arac gecildiðinde puaný 1 arttýrýr
        if (other.gameObject.CompareTag("basarýlýarac"))
        {
            trafficracer.Kopya.skor++;
        }
    }
}
