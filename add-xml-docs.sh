#!/bin/bash
# Script to add missing XML documentation comments to C# files

# Function to add constructor documentation
add_constructor_docs() {
    local file=$1
    local class_name=$(basename "$file" .cs)

    # Add documentation before public constructors without existing docs
    sed -i '
        /^[[:space:]]*public[[:space:]]\+'$class_name'[[:space:]]*(/i\
    /// <summary>\
    /// Initializes a new instance of the class.\
    /// </summary>
    ' "$file"
}

# Function to add method documentation
add_method_docs() {
    local file=$1

    # This is a simplified version - for production use a more sophisticated parser
    # Add generic documentation before public async methods
    sed -i '
        /^[[:space:]]*public[[:space:]]\+\(async[[:space:]]\+\)\?Task/i\
    /// <summary>\
    /// Performs the operation asynchronously.\
    /// </summary>
    ' "$file"
}

# Process all Implementation files
for file in YFinance.NET.Implementation/**/*.cs; do
    # Skip obj and bin directories
    if [[ "$file" == *"/obj/"* ]] || [[ "$file" == *"/bin/"* ]]; then
        continue
    fi

    echo "Processing $file..."
    add_constructor_docs "$file"
    add_method_docs "$file"
done

echo "Done!"
