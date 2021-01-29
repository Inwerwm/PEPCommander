﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVCommand.Commands
{
    public interface IUVCommand
    {
        /// <summary>
        /// コマンド名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 説明
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 実行
        /// </summary>
        void Do();
        /// <summary>
        /// もとに戻す
        /// </summary>
        void UnDo();
    }
}
