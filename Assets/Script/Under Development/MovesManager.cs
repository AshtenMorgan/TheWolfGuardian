//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MovesManager : MonoBehaviour
//{
//    private GameManager gm;
//    [SerializeField] List<Move> availableMoves;
//    [SerializeField] Controller controller;
//    [SerializeField] InputRecorder inputRecorder;
//    // Start is called before the first frame update
//    void OnEnable()
//    {
//        gm = GameManager.Instance;
//        if(gm.player != null)
//        {
//            controller = gm.player.GetComponent<PlayerControllerV2>();
//            inputRecorder = gm.playerRecorder;
//        }
        

//        availableMoves.Sort(Compare); //Sorts all the moves based on their priority
//    }

//    public bool IsMoveAvailable(List<KeyCode> keycodes) 
//    {
//        foreach (Move move in availableMoves) 
//        {
//            if (move.IsMoveAvailable(keycodes))
//                return true;
//        }
//        return false;
//    }

//    //public void PlayMove(List<KeyCode> keycodes) 
//    //{
//    //    foreach (Move move in availableMoves) 
//    //    {
//    //        if (move.IsMoveAvailable(keycodes)) 
//    //        {
//    //            controller.PlayMove(move.GetMove(), move.GetMoveComboPriority());
//    //            break;
//    //        }
//    //    }
//    //}

//    public int Compare(Move move1, Move move2) 
//    {
//        return Comparer<int>.Default.Compare(move2.GetMoveComboPriority(), move1.GetMoveComboPriority());
//    }
//}
