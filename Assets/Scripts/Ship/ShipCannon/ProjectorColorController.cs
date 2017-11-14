using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorColorController : MonoBehaviour {

    [Header("Сolors of the projector depending on the state of the turret:")]
    public Color notSelect = Color.white;
    public Color aiming = Color.yellow;
    public Color aimed = Color.green;
    public Color aimImpossible = Color.red;
    public TrajectoryPredictor3D.ProjectorColors projectorColors;
    // Use this for initialization
    void Awake () {
        projectorColors.NotSelect = notSelect;
        projectorColors.Aiming = aiming;
        projectorColors.Aimed = aimed;
        projectorColors.AimImpossible = aimImpossible;
    }
	
}
