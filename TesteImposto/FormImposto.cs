using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Imposto.Application;
using Imposto.Application.ViewModels;
using Imposto.Domain.Core.Notifications;
using Imposto.Infra.CrossCutting.Util;
using Imposto.Infra.Ioc;
using SimpleInjector;
using TesteImposto.Enum;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private PedidoViewModel pedido = new PedidoViewModel();
        Container container = new Container();

        public FormImposto()
        {
            CarregarTela();

            BootStrapper.RegisterServices(container);
            container.Verify();
        }

        /// <summary>
        /// Inicializa as dependências para carregar a tela.
        /// </summary>
        private void CarregarTela()
        {
            Controls.Clear();
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;
            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
            CarregarCombo(comboEstadoOrigem);
            CarregarCombo(comboEstadoDestino);
            pedido = new PedidoViewModel();
        }

        /// <summary>
        /// Popula os combos que serão exibidos na tela.
        /// </summary>
        /// <param name="combo"></param>
        private void CarregarCombo(ComboBox combo)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Selecione um estado", "")
            };
            list.AddRange(EnumUtil.GetEnumSelectListDescriptionAndKey<EstadosEnum>()
                .OrderBy(x => x.Value));

            combo.DataSource = list;
            combo.DisplayMember = "Key";
            combo.ValueMember = "Value";
        }

        /// <summary>
        /// Redimensiona as colunas da tabela.
        /// </summary>
        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        /// <summary>
        /// Configura a tabela exibida na tela.
        /// </summary>
        /// <returns></returns>
        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
                     
            return table;
        }

        /// <summary>
        /// Evento disparado ao clicar no botão gerar nota fiscal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            var service = container.GetInstance<NotaFiscalAppService>();

            PopularPedido();

            DataTable table = (DataTable)dataGridViewPedidos.DataSource;

            PopularPedidoItem(table);

            var erros = service.GerarNotaFiscal(pedido);

            ExibirMensagem(erros);

        }

        /// <summary>
        /// Popula a classe Pedido Item com os valores preenchidos na tela.
        /// </summary>
        /// <param name="table"></param>
        private void PopularPedidoItem(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                var isBrinde = !string.IsNullOrEmpty(row["Brinde"].ToString()) && Convert.ToBoolean(row["Brinde"]);
                var valorItemPedido = !string.IsNullOrEmpty(row["Valor"].ToString())
                    ? Convert.ToDouble(row["Valor"].ToString())
                    : 0;
                pedido.ItensDoPedido.Add(
                    new PedidoItemViewModel
                    {
                        Brinde = isBrinde,
                        CodigoProduto = row["Codigo do produto"].ToString(),
                        NomeProduto = row["Nome do produto"].ToString(),
                        ValorItemPedido = (decimal?)valorItemPedido
                    });
            }
        }

        /// <summary>
        /// Popula a classe Pedido com os valores preenchidos na tela.
        /// </summary>
        private void PopularPedido()
        {
            pedido.EstadoOrigem = comboEstadoOrigem.SelectedValue.ToString();
            pedido.EstadoDestino = comboEstadoDestino.SelectedValue.ToString();
            pedido.NomeCliente = textBoxNomeCliente.Text;
        }

        /// <summary>
        /// Exibe a mensagem de retorno na telas.
        /// </summary>
        /// <param name="erros">Lista com as validações.</param>
        private void ExibirMensagem(List<Notification> erros)
        {
            if (erros.Any())
            {
                string msg = String.Join("\n", erros.Select(x => x.Value).ToArray());
                MessageBox.Show(msg);
            }
            else
            {
                CarregarTela();
                MessageBox.Show("Operação efetuada com sucesso");
            }
        }

        /// <summary>
        /// Responsável por validar o valor informado na celula do ValorItemPedido. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewPedidos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Aborta a validação caso não seja a coluna de valor decimal.
            if (e.ColumnIndex != 2) return;

            decimal output;
            var isDecimal = decimal.TryParse(e.FormattedValue.ToString(), out output);

            if (!isDecimal)
            {
                MessageBox.Show("Insira somente numérico !");
                e.Cancel = true;
            }
        }
    }
}
