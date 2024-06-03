namespace TCPSession.FSM.States

module FSMStateEnum =
    type FSMState = 
    | CLOSED = 0
    | LISTEN = 1
    | SYN_SENT = 2
    | SYN_RCVD = 3
    | ESTABLISHED = 4
    | CLOSE_WAIT = 5
    | LAST_ACK = 6
    | FIN_WAIT_1 = 7
    | FIN_WAIT_2 = 8
    | CLOSING = 9
    | TIME_WAIT = 10


