#!/bin/bash
# Script to add XML documentation to all C# files

set -e

# Counter
count=0

# Find all C# files in Implementation directory
find YFinance.NET.Implementation -name "*.cs" -not -path "*/obj/*" -not -path "*/bin/*" | while read -r file; do
    echo "Processing: $file"

    # Create backup
    cp "$file" "$file.bak"

    # Apply AWK script
    awk -f add-docs.awk "$file.bak" > "$file"

    # Remove backup if successful
    if [ $? -eq 0 ]; then
        rm "$file.bak"
        ((count++))
    else
        # Restore from backup if failed
        mv "$file.bak" "$file"
        echo "Failed to process: $file"
    fi
done

echo "Processed $count files"
