using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PulseEquation
{
    // Start is called before the first frame update
    public PulseEquation(int xSize, int zSize, float[] wVals, float n)
    {
        this.xSize = xSize;
        this.zSize = zSize;
        this.wVals = wVals;
        this.n = n;
        this.waveCount = wVals.Length;
        this.sigma = (float)zSize / 4.5f;
        initializeGuassianArray();
        updateKvals();
        foreach (float num in gaussian)
        {
            Debug.Log(num);
        }
    }

    private int xSize;
    private int zSize;
    private int waveCount;
    private float[] wVals;
    private float[] kVals;
    private float[] gaussian;
    private float[] wave;
    private float N;
    private float sigma;


    public float n
    {
        get { return N; }
        set
        {
            N = value;
            updateKvals();            
        }
    }

    private void updateKvals()
    {
        kVals = new float[waveCount]; 
        for (int i = 0; i<waveCount; i++)
        {
            kVals[i] = 20*wVals[i] * N/ xSize;
        }
    }

    public float y(int x, int z)
    {
        return gaussian[z] * wave[x];
    }

    private void initializeGuassianArray()
    {
        gaussian = new float[zSize + 1];
        for (int i = 0; i<=zSize; i++)
        {
            gaussian[i] = Mathf.Exp( -((i - (zSize / 2) ) * (i - (zSize / 2))) / (2 * (sigma * sigma))   );
        }
    }

    public void updateWaveArray(float t)
    {
        wave = new float[xSize + 1];
        Parallel.For(0, xSize + 1, x =>
        {
            waveLoop(t, x);
        });

        /*for (int x = 0; x<= xSize; x++)
        {
            float waveVal = 0;
            for (int i = 0; i < waveCount; i++)
            {
               
                float w = wVals[i];
                float k = kVals[i];                
                waveVal += Mathf.Sin(k * x - w * t);
            }
            wave[x] = waveVal;
        }*/
    }

    public void waveLoop(float t, int x) {
        Debug.Log(x);
        float waveVal = 0;
        for (int i = 0; i < waveCount; i++)
        {

            float w = wVals[i];
            float k = kVals[i];
            waveVal += Mathf.Sin(k * x - w * t);
        }
        wave[x] = waveVal;
        
    }
    

}