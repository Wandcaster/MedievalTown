using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training
{
    //strLV -> zaczynajac od 0, ile razy dano dan¹ statystyke
    private int str, strLV;
    private int vit, vitLV;
    private int agi, agiLV;
    private int inte, inteLV;
    private int stur, sturLV;
    private int wsd, wsdLV;

    public Training(int str, int vit, int agi, int inte, int stur, int wsd)
    {
        this.str = str;
        this.vit = vit;
        this.agi = agi;
        this.inte = inte;
        this.stur = stur;
        this.wsd = wsd;
        strLV = 0;
        vitLV = 0;
        agiLV = 0;
        inteLV = 0;
        sturLV = 0;
        wsdLV = 0;
    }

    public bool CheckSTR()
    {
        if (str > Requirements(strLV))
        {
            str -= Requirements(strLV);
            strLV++;
            return true;
        }
        return false;

    }
    public bool CheckVIT()
    {
        if (vit > Requirements(vitLV))
        {
            vit -= Requirements(vitLV);
            vitLV++;
            return true;
        }
        return false;

    }
    public bool CheckAGI()
    {
        if (agi > Requirements(agiLV))
        {
            agi -= Requirements(agiLV);
            agiLV++;
            return true;
        }
        return false;

    }
    public bool CheckINTE()
    {
        if (inte > Requirements(inteLV))
        {
            inte -= Requirements(inteLV);
            inteLV++;
            return true;
        }
        return false;

    }
    public bool CheckSTUR()
    {
        if (stur > Requirements(sturLV))
        {
            stur -= Requirements(sturLV);
            sturLV++;
            return true;
        }
        return false;

    }
    public bool CheckWSD()
    {
        if (wsd > Requirements(wsdLV))
        {
            wsd -= Requirements(wsdLV);
            wsdLV++;
            return true;
        }
        return false;

    }







    public void AddAll(Training train)
    {
        str += train.str;
        vit += train.vit;
        agi += train.agi;
        inte += train.inte;
        stur += train.stur;
        wsd += train.wsd;
    }







    private int Requirements(int lv)
    {
        return 10000 + 1000 * lv;
    }


}
