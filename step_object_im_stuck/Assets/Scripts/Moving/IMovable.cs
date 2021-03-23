﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable 
{
    void Move(Vector3 direction, bool obstacle = false);
    void Rotate(Vector3 direction);

}