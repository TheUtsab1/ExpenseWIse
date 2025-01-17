using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using ExpenseWise2.Components.Model;
using ExpenseWise2.Components;

//namespace ExpenseWise2.Components.Service

public class TagService
{
    // Method to add a new tag
    public static void AddTag(TagModel tag)
    {
        try
        {
            if (tag == null || string.IsNullOrWhiteSpace(tag.Name))
            {
                Debug.WriteLine("Invalid tag. Cannot add.");
                return;
            }

            var json = JsonSerializer.Serialize(tag);
            File.AppendAllText(Util.TAG, json + Environment.NewLine);  // Ensure TAG file is being written to correctly

            Debug.WriteLine($"Tag added: {json}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding tag: {ex.Message}");
        }
    }


    public static List<TagModel> GetTags()
    {
        try
        {
            // Read all lines from the transaction file
            var allTags = File.ReadAllLines(Util.TAG);

            // Deserialize each JSON line into a TransactionModel object
            var tagList = allTags
                .Where(line => !string.IsNullOrWhiteSpace(line)) // Skip empty lines
                .Select(line => JsonSerializer.Deserialize<TagModel>(line))
                .ToList();

            return tagList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error reading transactions: {ex.Message}");
            return new List<TagModel>(); // Return an empty list if an error occurs
        }
    }


    // Method to remove a specific tag by its name
    public static void RemoveTag(string tagName)
    {
        try
        {
            // Retrieve all tags
            var tags = GetTags();

            // Find the tag to remove
            var tagToRemove = tags.FirstOrDefault(t => t.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase));

            if (tagToRemove != null)
            {
                tags.Remove(tagToRemove);

                // Save updated tags back to the file
                SaveTags(tags);

                Debug.WriteLine($"Tag removed: {tagName}");
            }
            else
            {
                Debug.WriteLine($"Tag not found: {tagName}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error removing tag: {ex.Message}");
        }
    }

    // Method to save all tags back to the file
    private static void SaveTags(List<TagModel> tags)
    {
        try
        {
            // Serialize all tags to JSON format and write to file
            var jsonString = string.Join(Environment.NewLine, tags.Select(t => JsonSerializer.Serialize(t)));
            File.WriteAllText(Util.TAG, jsonString);

            Debug.WriteLine("Tags saved successfully.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving tags: {ex.Message}");
        }
    }
}

