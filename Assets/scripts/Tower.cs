using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    protected string type;
    [SerializeField]
    private int cost;
    private Node location;
    protected Transform target;
    protected float turnSpeed = 20f;
    protected string enemytag = "Enemy";
    protected float fireCountdown = 0f;
    [SerializeField]
    protected float range;
    [SerializeField]
    protected float fireRate;
    [SerializeField]
    protected int firePower;
    [SerializeField]
    protected Transform rotate;


    public void RangeUpdate()
    {
        range += Location.Level * 2;
    }

    public int Cost { get => cost; set => cost = value; }
    public Node Location { get => location; set => location = value; }

    public virtual Tower GetUpgrade1()
    {
        return null;
    }
    public virtual Tower GetUpgrade2()
    {
        return null;
    }
    public virtual Tower GetUpgrade3()
    {
        return null;
    }

    public virtual void DoUpgrade1()
    {

    }
    public virtual void DoUpgrade2()
    {

    }
    public virtual void DoUpgrade3()
    {
 
    }

    public string GetTowerType()
    {
    return type;
    }
}
