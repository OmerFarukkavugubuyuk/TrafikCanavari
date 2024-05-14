using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class trafficracer  : MonoBehaviour
{
    
    public static trafficracer Kopya { get; private set; }
    private void Awake()
    {
        if (Kopya != null && Kopya != this)
        {
            Destroy(this);
            return;
        }
        Kopya = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public bool baslatici=false;
    public GameObject baslabutton;
    public GameObject korna;
    public GameObject yenidenbaslaButonu;
    public GameObject baslamaEkraný;

    public AudioSource seskutusu;
    public AudioClip arabasesi;
    public AudioClip kornasesi;
    public AudioClip kazasesi;

    public int skor;
    public Text skortext;
    public Text oyunbitti;
    public GameObject aracýmýz;

    public Vector3 eksenler;

    public GameObject yol;
    public float gecenyol = 0.0f;
    public float spawnzamani = 1.0f;
    public float aracspawnzamani = 1.0f;

    public float yolHizi;//yolun ilerleme hýzý
    public Vector3 spawnlocation;//yolun spawnlanacaðý konum
    public Quaternion spawnRotation;//yolun yönü
    public GameObject spawneryol;//örnek alýnacak yol

    public List<GameObject> yollar = new List<GameObject>();//üretilen yollarýn tutulduðu liste

    public List<GameObject> araclar = new List<GameObject>();//rastgele üretilecek araclarýn tutulduðu liste
    public List<GameObject> aktifAraclar = new List<GameObject>();//üretilen araclarýn tutulduðu liste
    public float aracHizi;//araclarýn ilerleme hizi
    public float gecenarac;
    public int aracAraligi;

    public Rigidbody fiziksistemi;
    public int harekethýzý;

    void Start()
    {
        yolspawn();//calisiyo direkt spawnlanýyo
        skortext.text = "skor= " + skor;
        oyunbitti.text = " ";
        
    }

    public void durdurmabuton() //oyunu durdurur ve basla butonunu ekrana cýkartýr
    {
        baslatici = false;
        baslabutton.gameObject.SetActive(true);//basla butonu acýk
        fiziksistemi.ResetInertiaTensor();//aracýmýzýn üzerindeki kuvvetleri sýfýrlar
        fiziksistemi.velocity = Vector3.zero;//ayný þekilde
        fiziksistemi.angularVelocity = Vector3.zero;//ayný 
    }
    
    public void baslatbutonu()//oyunu baslatýr ve basla butonunu yok eder
        {
            baslatici=true;
            Debug.Log("tuþ çalýþtý");
            baslabutton.gameObject.SetActive(false);//basla butonu kapalý
            oyunbitti.text= " ";
            baslamaEkraný.gameObject.SetActive(false);

        }
    public void yenidenbasla() 
    {
        baslatici = true;
        skor = 0;//skoru sýfýrlar
        aracýmýz.transform.position = new Vector3(0, 0, -11);//aracýmýzý ortaya getirir
        yenidenbaslaButonu.gameObject.SetActive(false);
        oyunbitti.text = " ";
    }
    public void kornabutonu() 
    {
        seskutusu.PlayOneShot(kornasesi);
    }

    void Update()
    {
        if (baslatici==true)
        {
               
                sag();
                sol();

                //skoru ekrana yazdýrýr
                skortext.text = "skor= " + skor;

                //calisiyo ilk yol hareket ediyo
                eksenler = yol.transform.position;
                eksenler.z -= Time.deltaTime * yolHizi;
                yol.transform.position = eksenler;


                gecenyol += Time.deltaTime;
                gecenarac += Time.deltaTime;
       
                if (gecenyol >= spawnzamani)//calisiyo
                {
                    yolspawn();
                    gecenyol = 0;
                }

        
                if (gecenarac >= aracspawnzamani)//calisiyo
                {
                    aracspawn();
                    gecenarac = 0;
                }
        
                hareket();//calisiyo
        }
        
        
    }
    void aracspawn()
    {
        int randomIndex = Random.Range(0, araclar.Count);
        GameObject secilenarac = araclar[randomIndex];

        GameObject klon = Instantiate(secilenarac, Randomkonum(-6, 6, 111, 0), spawnRotation);
        aktifAraclar.Add(klon);
        Debug.Log("deneme arac spawn");
    }
    Vector3 Randomkonum(float xMin, float xMax, float zPosition, float yPosition)//araclarýn oluþacaðý random konumu atýyor
    {
        float randomX = Random.Range(xMin, xMax);
        return new Vector3(randomX, yPosition, zPosition);


    }
    public void yolspawn() 
    {
        GameObject klonyol = Instantiate(spawneryol, spawnlocation, spawnRotation);
        yollar.Add(klonyol);

    }
    public void hareket()
    {
        foreach (GameObject klon in aktifAraclar)
        {

            eksenler = klon.transform.position;
            eksenler.z -= Time.deltaTime * aracHizi;
            klon.transform.position = eksenler;
        }
        foreach (GameObject klonyol in yollar)
        {
            Debug.Log("yol hareket");
            eksenler = klonyol.transform.position;
            eksenler.z -= Time.deltaTime * yolHizi;
            klonyol.transform.position = eksenler;

        }
    }
    public void sag()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            fiziksistemi.AddForce(Vector3.right * harekethýzý, ForceMode.Force);
            Debug.Log("sag calýstý");
        }
    }
    public void sol() 
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            fiziksistemi.AddForce(Vector3.left*harekethýzý,ForceMode.Force);
            Debug.Log("sol calýstý");
        }
    
    }
    }
