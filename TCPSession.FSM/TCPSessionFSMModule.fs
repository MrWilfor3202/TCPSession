namespace TCPSession.FSM

open System
open TCPSession.FSM.Events.FSMEventEnum
open TCPSession.FSM.States.FSMStateEnum

module TCPSessionFSMModule =
type TCPSession () =
    
    let transitionTable = Map[
        FSMState.CLOSED, Map[
            FSMEvent.APP_PASSIVE_OPEN, FSMState.LISTEN;
            FSMEvent.APP_ACTIVE_OPEN, FSMState.SYN_SENT
        ];

        FSMState.LISTEN, Map[
            FSMEvent.RCV_SYN, FSMState.SYN_RCVD;
            FSMEvent.APP_SEND, FSMState.SYN_SENT;
            FSMEvent.APP_CLOSE, FSMState.CLOSED;
        ];

        FSMState.SYN_RCVD, Map[
            FSMEvent.APP_CLOSE, FSMState.FIN_WAIT_1;
            FSMEvent.RCV_ACK, FSMState.ESTABLISHED;
        ];

         FSMState.SYN_SENT, Map[
            FSMEvent.RCV_SYN, FSMState.SYN_RCVD;
            FSMEvent.RCV_SYN_ACK, FSMState.ESTABLISHED;
            FSMEvent.APP_CLOSE, FSMState.CLOSED;
        ];

         FSMState.ESTABLISHED, Map[
            FSMEvent.APP_CLOSE, FSMState.FIN_WAIT_1;
            FSMEvent.RCV_FIN, FSMState.CLOSE_WAIT;
        ];

         FSMState.FIN_WAIT_1, Map[
            FSMEvent.RCV_FIN, FSMState.CLOSING;
            FSMEvent.RCV_FIN_ACK, FSMState.TIME_WAIT;
            FSMEvent.RCV_ACK, FSMState.FIN_WAIT_2;
        ];

         FSMState.CLOSING, Map[FSMEvent.RCV_ACK, FSMState.TIME_WAIT];

         FSMState.FIN_WAIT_2, Map[FSMEvent.RCV_FIN, FSMState.TIME_WAIT];

         FSMState.TIME_WAIT, Map[FSMEvent.APP_TIMEOUT, FSMState.CLOSED];

         FSMState.CLOSE_WAIT, Map[FSMEvent.APP_CLOSE, FSMState.LAST_ACK];

         FSMState.LAST_ACK, Map[FSMEvent.RCV_ACK, FSMState.CLOSED];
    ]

    let mutable currentState = FSMState.CLOSED

    member _.HandleEvent(event : FSMEvent) : string =
        if transitionTable.ContainsKey currentState then
            if transitionTable[currentState].ContainsKey event then
                currentState <- transitionTable[currentState][event]
                Enum.GetName(currentState).ToString()
            else
                "ERROR"
        else
            "ERROR"


    