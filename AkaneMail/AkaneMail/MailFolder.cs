﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;

namespace AkaneMail
{
    /// <summary>
    /// メールを保存しておく、名前のついたフォルダーを表します。
    /// </summary>
    public class MailFolder : IEnumerable<Mail>, INotifyPropertyChanged, INotifyPropertyChanging
    {
        private List<Mail> _mails;

        private string _name;
        private bool _canRename = true;

        /// <summary>
        /// MailFolder の名前を取得または設定します。
        /// </summary>
        /// <exception cref="InvalidOperationException">リネームは許可されていません。</exception>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == _name)
                    return;
                if (!CanRename)
                    throw new InvalidOperationException("リネームは許可されていません。");
                OnPropertyChanging(new PropertyChangingEventArgs("Name"));
                _name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }

        /// <summary>
        /// MailFolder に格納されているメールの数を取得します。
        /// </summary>
        public int Count { get { return _mails.Count; } }

        public bool CanRename
        {
            get { return _canRename; }
            set
            {
                if (value == _canRename)
                    return;
                OnPropertyChanging(new PropertyChangingEventArgs("CanRename"));
                _canRename = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CanRename"));
            }
        }

        /// <summary>
        /// プロパティ値が変更されたときに発生します。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        /// <summary>
        /// プロパティ値が変更されようとしているときに発生します。
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;
        protected void OnPropertyChanging(PropertyChangingEventArgs e)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, e);
        }

        /// <summary>
        /// 指定された名前で MailFolder クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MailFolder(string name)
        {
            _name = name;
            _mails = new List<Mail>();
        }

        /// <summary>
        /// 指定したインデックスにあるメールを取得します。
        /// </summary>
        /// <param name="index">取得するメールの 0 から始まるインデックス。</param>
        /// <returns>指定したインデックスにあるメール。</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> が 0 未満または <see cref="Count"/> 以上です。</exception>
        public Mail this[int index]
        {
            get
            {
                return _mails[index];
            }
        }

        /// <summary>
        /// MailFolder を反復処理する列挙子を返します。
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Mail> GetEnumerator()
        {
            return _mails.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// MailFolder の末尾にメールを追加します。
        /// </summary>
        /// <param name="mail">追加するメール。</param>
        public void Add(Mail mail)
        {
            _mails.Add(mail);
        }

        /// <summary>
        /// MailFolder からすべての Mail を削除します。
        /// </summary>
        public void Clear()
        {
            _mails.Clear();
        }

        /// <summary>
        /// MaliFolderの中から、最初に見つかった指定されたメール削除します。
        /// <remarks>
        /// このメソッドはO(n)です。計算量に十分注意して使用してください。
        /// </remarks>
        /// <param name="mail">削除するメール。</param>
        /// <returns>削除された場合は true。それ以外の場合はfalse。<paramref name="mail"/> が見つからなかった場合にもfalseを返します。</returns>
        public bool Remove(Mail mail)
        {
            return _mails.Remove(mail);
        }

        /// <summary>
        /// MailFolderの指定されたインデックスにあるメールをゴミ箱に移動します。
        /// </summary>
        /// <remarks>
        /// このメソッドはO(n)です。計算量に十分注意して使用してください。
        /// </remarks>
        /// <param name="index">削除するメールの 0 から始まるインデックス。</param>
        /// <exception cref="ArgumentOutOfRangeException">indexが 0 未満または <see cref="Count"/> 以上です。</exception>
        public void RemoveAt(int index)
        {
            try {
                _mails.RemoveAt(index);
            }
            catch (ArgumentOutOfRangeException) {
                throw;
            }
        }
    }
}
