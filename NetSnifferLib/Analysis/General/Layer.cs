﻿namespace NetSnifferLib.Analysis.General
{
    enum Layer
    {
        Physical,
        DataLink,
        BetweenDataLinkAndNetwrok,
        Network,
        BetweenNetwrokAndTransport,
        Trasport,
        Session,
        Presentation,
        Application
    }
}