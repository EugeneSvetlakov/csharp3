using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MailSender.View
{
    /// <summary>
    /// Логика взаимодействия для ListControler.xaml
    /// </summary>
    public partial class ListControler : UserControl //, INotifyPropertyChanged
    {
        #region Обычное добавление свойств
        //private string _PanelName;

        //public string PanelName
        //{
        //    get => _PanelName;
        //    set
        //    {
        //        _PanelName = value;
        //        OnPropertyChanhed();
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanhed([CallerMemberName] string PropertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        //}
        #endregion

        #region Свойство зависимости: PanelName : string - Название панели
        /// <summary>Название панели</summary>
        public static readonly DependencyProperty PanelNameProperty =
            DependencyProperty.Register(
                nameof(PanelName),
                typeof(string),
                typeof(ListControler),
                new PropertyMetadata(default(string))
                );
        /// <summary>Название панели</summary>
        public string PanelName
        {
            get => (string) GetValue(PanelNameProperty);
            set => SetValue(PanelNameProperty, value);
        }
        #endregion

        #region Свойство зависимости: ItemSource : IEnumerable - Источник данных
        /// <summary>Источник данных</summary>
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(
                "ItemSource",
                typeof(IEnumerable),
                typeof(ListControler),
                new PropertyMetadata(default(IEnumerable))
                );
        /// <summary>Источник данных</summary>
        public IEnumerable ItemSource
        {
            get => (IEnumerable)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }
        #endregion

        #region Свойство зависимости: SelectedItem : Object - Выбранный элемент
        /// <summary>Выбранный элемент</summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                "SelectedItem",
                typeof(Object),
                typeof(ListControler),
                new PropertyMetadata(default(Object))
                );
        /// <summary>Выбранный элемент</summary>
        public Object SelectedItem
        {
            get => (Object)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        #endregion

        #region Свойство зависимости: SelectedIndex : Object - Номер выбранного элемента
        /// <summary>Выбранный элемент</summary>
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register(
                "SelectedIdex",
                typeof(int),
                typeof(ListControler),
                new PropertyMetadata(default(int))
                );
        /// <summary>Выбранный элемент</summary>
        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }
        #endregion

        #region Свойство зависимости: CreateCommand : ICommand - Создать новый элемент
        /// <summary>Команда - Создать новый элемент</summary>
        public static readonly DependencyProperty CreateCommandProperty =
            DependencyProperty.Register(
                nameof(CreateCommand),
                typeof(ICommand),
                typeof(ListControler),
                new PropertyMetadata(default(ICommand))
                );
        /// <summary>Команда - Создать новый элемент</summary>
        public ICommand CreateCommand
        {
            get => (ICommand)GetValue(CreateCommandProperty);
            set => SetValue(CreateCommandProperty, value);
        }
        #endregion

        #region Свойство зависимости: EditCommand : ICommand - Редактировать элемент
        /// <summary>Команда - Создать новый элемент</summary>
        public static readonly DependencyProperty EditCommandProperty =
            DependencyProperty.Register(
                nameof(EditCommand),
                typeof(ICommand),
                typeof(ListControler),
                new PropertyMetadata(default(ICommand))
                );
        /// <summary>Команда - Редактировать элемент</summary>
        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }
        #endregion

        #region Свойство зависимости: DeleteCommand : ICommand - Удалить элемент
        /// <summary>Команда - Удалить элемент</summary>
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(
                nameof(DeleteCommand),
                typeof(ICommand),
                typeof(ListControler),
                new PropertyMetadata(default(ICommand))
                );
        /// <summary>Команда - Удалить элемент</summary>
        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }
        #endregion

        #region Свойство зависимости: ItemTemplate : DataTemplate - Шаблон визуализации данных
        /// <summary>Свойство - Шаблон визуализации данных</summary>
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(
                "ItemTemplate",
                typeof(DataTemplate),
                typeof(ListControler),
                new PropertyMetadata(default(DataTemplate))
                );
        /// <summary>Свойство - Шаблон визуализации данных</summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }
        #endregion

        public ListControler()
        {
            InitializeComponent();
        }
    }
}
