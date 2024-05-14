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
        //araclara ya da yanlara de�ince oyunu bitirir
        if (other.gameObject.CompareTag("zemin"))
        {
            trafficracer.Kopya.seskutusu.PlayOneShot(trafficracer.Kopya.kazasesi);//kaza sesini �alar
            //trafficracer.Kopya.skor = 0;//skoru s�f�rlar
            trafficracer.Kopya.baslatici = false;//oyunu durdurur
            //trafficracer.Kopya.baslabutton.gameObject.SetActive(true);//basla butonunu g�sterir
            trafficracer.Kopya.oyunbitti.text = "OYUN B�TT� ";//ekrana oyun bitti yazd�r�r
            
            foreach (GameObject klon in trafficracer.Kopya.aktifAraclar)
            {
                foreach (GameObject item in trafficracer.Kopya.araclar)
                {
                    if (item != klon)
                    {
                        Destroy(klon);//araclar� yok eder
                        Debug.Log("sfkmlds");
                    }
                }
            }
            trafficracer.Kopya.aktifAraclar.Clear();//aktifaraclar listesini temizler
           //trafficracer.Kopya.arac�m�z.transform.position = new Vector3(0, 0, -11);//arac�m�z� ortaya getirir
            trafficracer.Kopya.fiziksistemi.ResetInertiaTensor();//arac�m�z�n �zerindeki kuvvetleri s�f�rlar
            trafficracer.Kopya.fiziksistemi.velocity = Vector3.zero;//ayn� �ekilde
            trafficracer.Kopya.fiziksistemi.angularVelocity = Vector3.zero;//ayn� 
            trafficracer.Kopya.yenidenbaslaButonu.gameObject.SetActive(true);//yeniden ba�lama butonunu g�sterir
        }

        //her arac gecildi�inde puan� 1 artt�r�r
        if (other.gameObject.CompareTag("basar�l�arac"))
        {
            trafficracer.Kopya.skor++;
        }
    }
}
