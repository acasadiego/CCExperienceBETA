using System.Collections;
using System.Collections.Generic;

public class Mision
{
    private string nombre, variable, descripcion;
    private int nivel,probabilidadExito, costo, indexPais, duracion, recompensa;

    private double valorExito;

    public Mision(string nombre, string variable, string descripcion, int nivel, int probabilidadExito, int costo, int indexPais, int duracion, int recompensa, double valorExito)
    {
        this.nombre = nombre;
        this.indexPais = indexPais;
        this.variable = variable;
        this.descripcion = descripcion;
        this.nivel = nivel;
        this.probabilidadExito = probabilidadExito;
        this.costo = costo;
        this.valorExito = valorExito;
        this.duracion = duracion;
        this.recompensa = recompensa;
    }

    public string getNombre()
    {
        return nombre;
    }
    
    public int getIndexPais()
    {
        return indexPais;
    }

    public string getVariable()
    {
        return variable;
    }

    public string getDescripcion()
    {
        return descripcion;
    }

    public int getNivel()
    {
        return nivel;
    }

    public int getProbabilidadExito()
    {
        return probabilidadExito;
    }

    public int getCosto()
    {
        return costo;
    }

    public double getValorExito()
    {
        return valorExito;
    }

    public int getDuracion()
    {
        return duracion;
    }

    public int getRecompensa()
    {
        return recompensa;
    }

}