//  BestCS Core Library
// BestCS.NET framework
// http://www.sf.net/projects/BestCS/
//
// Copyright © BestCS.NET, 2007-2011
// dinruspro@mail.ru
//

namespace  BestCS
{
    using System;

    /// <summary>
    ///Делегат, используемый событиями для сообщения о том,
   /// что событие получено или отправлено.
    /// </summary>
    /// 
    /// <param name="sender"> Отправитель события.</param>
    /// <param name="eventArgs">Аргументы события, содержащие детали о переданном сообщении.</param>
    ///
    public delegate void MessageTransferHandler( object sender, CommunicationBufferEventArgs eventArgs );
}
