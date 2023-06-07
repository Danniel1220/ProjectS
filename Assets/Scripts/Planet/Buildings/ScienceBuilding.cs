using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScienceBuilding
{
    public ScienceBuildingType buildingType;
    public int researchSpeed;

    public enum ScienceBuildingType { ResearchTeam, Laboratory, ExperimentationFacility };

    public ScienceBuilding(ScienceBuildingType buildingType)
    {
        this.buildingType = buildingType;

        // assigning the storage slots depending on what type of habitat building this is
        switch (buildingType)
        {
            case ScienceBuildingType.ResearchTeam:
                researchSpeed = 1;
                break;
            case ScienceBuildingType.Laboratory:
                researchSpeed = 4;
                break;
            case ScienceBuildingType.ExperimentationFacility:
                researchSpeed = 8;
                break;
        }
    }
}
