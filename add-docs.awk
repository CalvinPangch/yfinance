#!/usr/bin/awk -f
# AWK script to add XML documentation to C# files

BEGIN {
    prev_line = ""
    needs_doc = 0
}

# Check if previous line needs documentation
function needs_documentation(line) {
    # Skip if already has documentation
    if (match(prev_line, /^[[:space:]]*\/\/\//)) return 0

    # Check for public declarations
    if (match(line, /^[[:space:]]*public[[:space:]]/)) {
        # Skip if it's a class, struct, interface, enum declaration (already has doc)
        if (match(line, /(class|struct|interface|enum)[[:space:]]/)) return 0
        return 1
    }
    return 0
}

# Get indentation
function get_indent(line) {
    match(line, /^[[:space:]]*/)
    return substr(line, RSTART, RLENGTH)
}

# Check if line is a constructor
function is_constructor(line, class_name) {
    pattern = "public[[:space:]]+" class_name "[[:space:]]*\\("
    return match(line, pattern)
}

# Check if line is a method
function is_method(line) {
    return match(line, /public[[:space:]]+(async[[:space:]]+)?Task/)
}

# Check if line is a property
function is_property(line) {
    return match(line, /public[[:space:]]+[^=]+\{[[:space:]]*(get|set)/)
}

# Add constructor documentation
function add_constructor_doc(indent, class_name) {
    print indent "/// <summary>"
    print indent "/// Initializes a new instance of the <see cref=\"" class_name "\"/> class."
    print indent "/// </summary>"
}

# Add method documentation
function add_method_doc(indent) {
    print indent "/// <summary>"
    print indent "/// Performs the requested operation."
    print indent "/// </summary>"
}

# Add property documentation
function add_property_doc(indent) {
    print indent "/// <summary>"
    print indent "/// Gets or sets the value."
    print indent "/// </summary>"
}

{
    current_line = $0

    # Track class name
    if (match(current_line, /(public|internal)[[:space:]]+(sealed[[:space:]]+)?class[[:space:]]+([A-Za-z0-9_]+)/)) {
        # Extract class name
        split(current_line, parts, /class[[:space:]]+/)
        if (length(parts) >= 2) {
            split(parts[2], name_parts, /[[:space:](]/)
            current_class = name_parts[1]
        }
    }

    # Check if we need to add documentation
    if (needs_documentation(current_line)) {
        indent = get_indent(current_line)

        if (current_class != "" && is_constructor(current_line, current_class)) {
            add_constructor_doc(indent, current_class)
        } else if (is_method(current_line)) {
            add_method_doc(indent)
        } else if (is_property(current_line)) {
            add_property_doc(indent)
        }
    }

    print current_line
    prev_line = current_line
}
