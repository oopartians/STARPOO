using UnityEngine;
using System.Collections.Generic;

public abstract class CollisionPender : INumberable
{
    static int numberring = 0;
    [HideInInspector]
    public int colNum = 0;
    public struct Col
    {
        public bool enter;
        public Collider2D cd;
        public int number;
        public Col(Collider2D cd, bool enter, int number)
        {
            this.enter = enter;
            this.cd = cd;
            this.number = number;
        }
    }

    protected List<Col> cols = new List<Col>();


    protected abstract void VirtualOnTriggerEnter2D(Collider2D cd);
    protected abstract void VirtualOnTriggerExit2D(Collider2D cd);


    public virtual void DoCollision()
    {
        while (cols.Count > 0)
        {
            var col = cols[0];
            cols.RemoveAt(0);
            if (col.enter)
            {
                VirtualOnTriggerEnter2D(col.cd);
            }
            else
            {
                VirtualOnTriggerExit2D(col.cd);
            }
        }
    }


    protected void OnTriggerEnter2D(Collider2D cd)
    {
        var tag = cd.tag;
        if (tag != "Ship" && tag != "Bullet" && tag != "Radar")
        {
            return;
        }

        cols.Add(new Col(cd, true, colNum++));
    }

    protected void OnTriggerExit2D(Collider2D cd)
    {
        var tag = cd.tag;
        if (tag != "Ship" && tag != "Bullet" && tag != "Radar" && tag != "Ground")
        {
            return;
        }
        cols.Add(new Col(cd, false, colNum++));
    }
}
