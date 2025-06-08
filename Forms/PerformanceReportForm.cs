using Course.DatabaseManager;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Course.Forms
{
    public partial class PerformanceReportForm : Form
    {
        private readonly StudentManager _studentManager;
        private readonly PerformanceManager _performanceManager;
        private readonly ClassManager _classManager;
        private readonly MainForm _mainForm;
        public PerformanceReportForm(MainForm mainForm)
        {
            InitializeComponent();
            InitializeDataGridViewColumns();
            // Инициализация менеджеров
            _studentManager = new StudentManager();
            _performanceManager = new PerformanceManager();
            _classManager = new ClassManager();
            _mainForm = mainForm;

            SetupControls();
            LoadFilters();
        }

        /// <summary>
        /// Устанавливает значения по умолчанию для DataGridView и DateTimePicker.
        /// </summary>
        private void SetupControls()
        {
            // Настройка DataGridView
            dgvReport.AutoGenerateColumns = false;
            dgvReport.AllowUserToAddRows = false;
            dgvReport.ReadOnly = true;

            // Настройка DateTimePicker
            dtpStart.Value = DateTime.Today.AddMonths(-1);
            dtpEnd.Value = DateTime.Today;
            dtpEnd.MaxDate = DateTime.Today;
        }

        /// <summary>
        /// Загружает список классов и предметов из базы данных в комбобоксы.
        /// </summary>
        private void LoadFilters()
        {
            try
            {
                // Загрузка классов из базы
                var classes = _classManager.GetAllClasses();
                cmbClass.DataSource = classes;
                cmbClass.DisplayMember = "ClassName";
                cmbClass.ValueMember = "ClassId";

                // Загрузка предметов из таблицы SchoolPerformance
                DataTable subjects = _performanceManager.GetAllSubjects();
                cmbSubject.DataSource = subjects;
                cmbSubject.DisplayMember = "SubjectName";
                cmbSubject.ValueMember = "SubjectName"; // Используем имя предмета как значение
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки фильтров: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Настраивает столбцы DataGridView для отображения данных об успеваемости.
        /// </summary>
        private void InitializeDataGridViewColumns()
        {
            // Очищаем существующие столбцы
            dgvReport.Columns.Clear();

            // Создаем и настраиваем столбцы вручную
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "StudentId",
                HeaderText = "ID студента",
                DataPropertyName = "StudentId",
                Visible = false // Скрытый столбец
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "StudentFullName",
                HeaderText = "ФИО студента",
                DataPropertyName = "StudentFullName",
                Width = 180,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ClassName",
                HeaderText = "Класс",
                DataPropertyName = "ClassName",
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SubjectName",
                HeaderText = "Предмет",
                DataPropertyName = "SubjectName",
                Width = 120
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "AverageGrade",
                HeaderText = "Средний балл",
                DataPropertyName = "AverageGrade",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Format = "N1" // Формат числа (1 знак после запятой)
                }
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "GradesCount",
                HeaderText = "Кол-во оценок",
                DataPropertyName = "GradesCount",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "AbsencesCount",
                HeaderText = "Пропуски",
                DataPropertyName = "AbsencesCount",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = System.Drawing.Color.Red // Красный цвет для пропусков
                }
            });

            // Настройка внешнего вида DataGridView
            dgvReport.EnableHeadersVisualStyles = false;
            dgvReport.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                BackColor = System.Drawing.Color.LightGray,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dgvReport.RowHeadersVisible = false; // Скрываем колонку с заголовками строк
            dgvReport.AllowUserToAddRows = false; // Запрещаем добавление новых строк
            dgvReport.ReadOnly = true; // Только для чтения
        }

        /// <summary>
        /// Генерирует отчет по заданным фильтрам и отображает в таблице.
        /// </summary>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                int? classId = cmbClass.SelectedValue as int?;
                string subjectName = cmbSubject.SelectedValue?.ToString();
                DateTime startDate = dtpStart.Value.Date;
                DateTime endDate = dtpEnd.Value.Date.AddDays(1).AddSeconds(-1);

                DataTable reportData = _performanceManager.GetPerformanceReport(
                    classId,
                    subjectName,
                    startDate,
                    endDate
                );

                if (reportData.Rows.Count == 0)
                {
                    MessageBox.Show("Нет данных для отображения.", "Информация",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvReport.DataSource = null; // Сброс привязки
                dgvReport.DataSource = reportData;
                FormatReportGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Форматирует отображение средней оценки (цвет ячейки) и авторазмер колонок.
        /// </summary>
        private void FormatReportGrid()
        {
            if (dgvReport.Columns.Contains("AverageGrade"))
            {
                foreach (DataGridViewRow row in dgvReport.Rows)
                {
                    if (row.Cells["AverageGrade"].Value != null &&
                        double.TryParse(row.Cells["AverageGrade"].Value.ToString(), out double avgGrade))
                    {
                        row.Cells["AverageGrade"].Style.BackColor = GetGradeColor(avgGrade);
                    }
                }
            }
            dgvReport.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        /// <summary>
        /// Возвращает цвет фона в зависимости от средней оценки.
        /// </summary>
        /// <param name="grade">Средняя оценка</param>
        /// <returns>Цвет ячейки</returns>
        private System.Drawing.Color GetGradeColor(double grade)
        {
            if (grade < 3) return System.Drawing.Color.LightCoral;
            if (grade < 4) return System.Drawing.Color.LightYellow;
            return System.Drawing.Color.LightGreen;
        }

        /// <summary>
        /// Обработка кнопки экспорта в CSV. Проверяет данные и вызывает ExportToCsv.
        /// </summary>
        private void btnExportExcel_Click_1(object sender, EventArgs e)
        {
            if (!(dgvReport.DataSource is DataTable dataTable) || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта. Сначала сформируйте отчет.",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Проверяем сам DataSource, а не строки в DataGridView
            var dataSource = dgvReport.DataSource as DataTable;

            if (dataSource == null || dataSource.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = $"Успеваемость_{DateTime.Now:yyyyMMdd_HHmm}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        ExportToCsv(dataSource, sfd.FileName);

                        // Открываем файл после сохранения
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = sfd.FileName,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        /// <summary>
        /// Сохраняет переданную таблицу в CSV-файл.
        /// </summary>
        /// <param name="data">Данные для экспорта</param>
        /// <param name="filePath">Путь сохранения</param>
        private void ExportToCsv(DataTable data, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                // Заголовки столбцов
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    sw.Write(data.Columns[i].ColumnName);
                    if (i < data.Columns.Count - 1)
                        sw.Write(";");
                }
                sw.WriteLine();

                // Данные
                foreach (DataRow row in data.Rows)
                {
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        string value = row[i].ToString();

                        // Экранируем специальные символы
                        if (value.Contains(";") || value.Contains("\"") || value.Contains("\n"))
                            value = $"\"{value.Replace("\"", "\"\"")}\"";

                        sw.Write(value);
                        if (i < data.Columns.Count - 1)
                            sw.Write(";");
                    }
                    sw.WriteLine();
                }
            }
        }

        /// <summary>
        /// Отображает отладочную информацию о текущем источнике данных.
        /// </summary>
        private void btnDebug_Click(object sender, EventArgs e)
        {
            StringBuilder debugInfo = new StringBuilder();

            // Проверяем DataSource
            if (dgvReport.DataSource == null)
            {
                debugInfo.AppendLine("DataSource = null");
            }
            else if (dgvReport.DataSource is DataTable table)
            {
                debugInfo.AppendLine($"Строк в DataTable: {table.Rows.Count}");
                debugInfo.AppendLine($"Столбцы: {string.Join(", ", table.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");

                if (table.Rows.Count > 0)
                {
                    var firstRow = table.Rows[0];
                    debugInfo.AppendLine("Первая строка:");
                    foreach (DataColumn col in table.Columns)
                    {
                        debugInfo.AppendLine($"{col.ColumnName}: {firstRow[col]}");
                    }
                }
            }
            else
            {
                debugInfo.AppendLine($"DataSource имеет тип: {dgvReport.DataSource.GetType().Name}");
            }

            MessageBox.Show(debugInfo.ToString(), "Диагностика данных");
        }

        /// <summary>
        /// Обработка кнопки экспорта в PDF. Проверяет данные и вызывает ExportToPdf.
        /// </summary>
        private void btnExportPdf_Click_1(object sender, EventArgs e)
        {
            if (!(dgvReport.DataSource is DataTable dataTable) || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта. Сначала сформируйте отчет.",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF files (*.pdf)|*.pdf";
                sfd.FileName = $"Успеваемость_{DateTime.Now:yyyyMMdd_HHmm}.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        ExportToPdf(dataTable, sfd.FileName);

                        // Открываем файл после сохранения
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = sfd.FileName,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при экспорте в PDF: {ex.Message}", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        /// <summary>
        /// Генерирует PDF-документ из таблицы успеваемости с помощью QuestPDF.
        /// </summary>
        /// <param name="data">Источник данных</param>
        /// <param name="filePath">Путь для сохранения PDF</param>
        private void ExportToPdf(DataTable data, string filePath)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var visibleColumns = dgvReport.Columns.Cast<DataGridViewColumn>()
                                            .Where(c => c.Visible)
                                            .ToList();

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White); // Исправлено здесь
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .AlignCenter()
                        .Text("Отчет об успеваемости")
                        .SemiBold().FontSize(16);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Table(table =>
                        {
                            // Настройка столбцов
                            table.ColumnsDefinition(columns =>
                            {
                                foreach (var column in visibleColumns)
                                {
                                    if (column.Name == "StudentFullName")
                                        columns.RelativeColumn(3);
                                    else if (column.Name == "SubjectName")
                                        columns.RelativeColumn(2);
                                    else
                                        columns.RelativeColumn();
                                }
                            });

                            // Заголовки таблицы
                            table.Header(header =>
                            {
                                foreach (var column in visibleColumns)
                                {
                                    header.Cell()
                                        .Background(Colors.Grey.Lighten3) // Исправлено здесь
                                        .Padding(5)
                                        .AlignCenter()
                                        .Text(column.HeaderText);
                                }
                            });

                            // Данные таблицы
                            foreach (DataRow row in data.Rows)
                            {
                                foreach (var column in visibleColumns)
                                {
                                    var value = row[column.DataPropertyName].ToString();

                                    table.Cell()
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten2) // Исправлено здесь
                                        .Padding(5)
                                        .AlignMiddle()
                                        .Text(text =>
                                        {
                                            if (column.Name == "AverageGrade" &&
                                                double.TryParse(value, out double grade))
                                            {
                                                text.Span(value)
                                                    .BackgroundColor(GetPdfGradeColor(grade));
                                            }
                                            else
                                            {
                                                text.Span(value);
                                            }

                                            if (column.Name == "AverageGrade" ||
                                                column.Name == "GradesCount" ||
                                                column.Name == "AbsencesCount")
                                            {
                                                text.AlignCenter();
                                            }
                                            else
                                            {
                                                text.AlignLeft();
                                            }
                                        });
                                }
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Страница ");
                            x.CurrentPageNumber();
                            x.Span(" из ");
                            x.TotalPages();
                            x.Span($" | Сформировано: {DateTime.Now:dd.MM.yyyy HH:mm}");
                        });
                });
            })
            .GeneratePdf(filePath);
        }

        /// <summary>
        /// Возвращает цвет фона текста в PDF-файле в зависимости от оценки.
        /// </summary>
        /// <param name="grade">Средняя оценка</param>
        /// <returns>Цвет фона текста</returns>
        private string GetPdfGradeColor(double grade)
        {
            if (grade < 3) return Colors.Red.Lighten4;
            if (grade < 4) return Colors.Yellow.Lighten4;
            return Colors.Green.Lighten4;
        }
    }
}