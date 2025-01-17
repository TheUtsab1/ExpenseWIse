using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using ExpenseWise2.Components.Model;

namespace ExpenseWise2.Components.Service
{
    public class TransactionService
    {
        public static void CreateTransaction(TransactionModel trans)
        {
            try
            {
                // Retrieve all transactions and add the new transaction
                var allTransactions = GetTransactions();
                allTransactions.Add(trans);

                // Serialize the updated list to JSON and overwrite the file
                var json = JsonSerializer.Serialize(allTransactions);
                File.WriteAllText(Util.TRANSACTION, json);

                Debug.WriteLine($"Transaction created: {JsonSerializer.Serialize(trans)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating transaction: {ex.Message}");
            }
        }

        public static List<TransactionModel> GetTransactions()
        {
            try
            {
                // Check if the file exists
                if (!File.Exists(Util.TRANSACTION))
                {
                    return new List<TransactionModel>();
                }

                // Read the JSON file content
                var json = File.ReadAllText(Util.TRANSACTION);

                // Deserialize the JSON into a list of transactions
                return JsonSerializer.Deserialize<List<TransactionModel>>(json) ?? new List<TransactionModel>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading transactions: {ex.Message}");
                return new List<TransactionModel>(); // Return an empty list if an error occurs
            }
        }

        public static List<TransactionModel> GetTransactions(TransactionType type)
        {
            try
            {
                // Filter transactions by the specified type
                return GetTransactions()
                    .Where(t => t.Type == type)
                    .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading transactions for {type}: {ex.Message}");
                return new List<TransactionModel>(); // Return an empty list if an error occurs
            }
        }

        public static double GetTotal(TransactionType type)
        {
            try
            {
                // Sum the amounts of transactions of the specified type
                return GetTransactions(type)
                    .Sum(t => t.Amount);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error calculating total for {type}: {ex.Message}");
                return 0; // Return 0 if an error occurs
            }
        }

        public static double GetTotal()
        {
            try
            {
                // Sum the amounts of all transactions
                return GetTransactions()
                    .Sum(t => t.Amount);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error calculating total: {ex.Message}");
                return 0; // Return 0 if an error occurs
            }
        }

        public static double GetDebt(bool isCleared = false)
        {
            double amount = 0;
            try
            {
                var transactions = GetTransactions(TransactionType.Debt);
                foreach(var each in transactions)
                {
                    if (each.IsCleared)
                    {
                        amount += each.Amount;
                    }
                }
                return amount;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        

        public static double GetBalance()
        {
            try
            {
                // Calculate the total income, total expense, and pending debt
                double totalIncome = GetTotal(TransactionType.Income);
                double totalExpense = GetTotal(TransactionType.Expense);
                double totalPendingDebt = GetTransactions(TransactionType.Debt)
                    .Where(t => !t.IsCleared)
                    .Sum(t => t.Amount);
                double totalClearedDebt = GetTransactions(TransactionType.Debt)
                    .Where(t => t.IsCleared)
                    .Sum(t => t.Amount); ;
                // Calculate the balance
                double balance = totalIncome - totalExpense + totalPendingDebt;

                return balance;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error calculating balance: {ex.Message}");
                return 0; // Return 0 if an error occurs
            }
        }
    }
}
