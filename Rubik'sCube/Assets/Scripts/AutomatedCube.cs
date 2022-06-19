using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedCube : MonoBehaviour
{
    public static List<MoveType> moveList = new List<MoveType>();
    private static readonly List<MoveType> allMoves = new List<MoveType>()
    {   new MoveType(){ move = "U", angle = -90 }, new MoveType(){ move = "U2", angle = 180 }, new MoveType(){ move = "U'", angle = 90 },
        new MoveType(){ move = "D", angle = -90 }, new MoveType(){ move = "D2", angle = 180 }, new MoveType(){ move = "D'", angle = 90 },
        new MoveType(){ move = "L", angle = -90 }, new MoveType(){ move = "L2", angle = 180 }, new MoveType(){ move = "L'", angle = 90 },
        new MoveType(){ move = "R", angle = -90 }, new MoveType(){ move = "R2", angle = 180 }, new MoveType(){ move = "R'", angle = 90 },
        new MoveType(){ move = "F", angle = -90 }, new MoveType(){ move = "F2", angle = 180 }, new MoveType(){ move = "F'", angle = 90 },
        new MoveType(){ move = "B", angle = -90 }, new MoveType(){ move = "B2", angle = 180 }, new MoveType(){ move = "B'", angle = 90 },
    };

    private CubeState cubeState;
    private ReadCubeSides readCube;

    private void Start()
    {
        cubeState = GetComponent<CubeState>();
        readCube = GetComponent<ReadCubeSides>();

        InitializeMovesSides();
    }

    private void Update()
    {
        if (moveList.Count > 0 && !CubeState.autoRotating && CubeState.started)
        {
            DoMove(moveList[0]);

            moveList.Remove(moveList[0]);
        }
    }

    public static MoveType StringToMoveType(string move)
    {
        foreach (MoveType moveType in allMoves)
        {
            if (moveType.move == move)
                return moveType;
        }

        return null;
    }

    private void InitializeMovesSides()
    {
        if (cubeState == null)
            return;

        for (int i = 0; i < 3; i++)
            allMoves[i].side = cubeState.up;

        for (int i = 3; i < 6; i++)
            allMoves[i].side = cubeState.down;

        for (int i = 6; i < 9; i++)
            allMoves[i].side = cubeState.left;

        for (int i = 9; i < 12; i++)
            allMoves[i].side = cubeState.right;

        for (int i = 12; i < 15; i++)
            allMoves[i].side = cubeState.front;

        for (int i = 15; i < 18; i++)
            allMoves[i].side = cubeState.back;
    }

    public void Shuffle()
    {
        List<MoveType> moves = new List<MoveType>();
        int shuffleLength = Random.Range(10, 30);

        for (int i = 0; i < shuffleLength; i++)
        {
            int randomMove = Random.Range(0, allMoves.Count);

            moves.Add(allMoves[randomMove]);
        }

        moveList = moves;
    }

    private void DoMove(MoveType move)
    {
        InitializeMovesSides();

        readCube.ReadCubeState();

        CubeState.autoRotating = true;

        RotateSide(move.side, move.angle);
    }

    private void RotateSide(List<GameObject> side, float angle)
    {
        PivotRotation pivotRotation = side[4].transform.parent.GetComponent<PivotRotation>();

        pivotRotation.StartAutoRotate(side, angle);
    }
}

public class MoveType
{
    public List<GameObject> side;
    public string move;
    public float angle;
}