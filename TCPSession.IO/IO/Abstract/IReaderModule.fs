namespace TCPSession.IO.Abstract
open TCPSession.FSM.Events

module IReaderModule =

    type public IReader =
        abstract member Read:unit -> FSMEventEnum.FSMEvent[]
