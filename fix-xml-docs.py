#!/usr/bin/env python3
"""
Script to add missing XML documentation comments to C# files.
This fixes CS1591 errors (missing XML comments for publicly visible types or members).
"""

import os
import re
from pathlib import Path

def get_indent(line):
    """Get the indentation of a line."""
    return len(line) - len(line.lstrip())

def has_xml_doc_before(lines, index):
    """Check if there's XML documentation before the given line."""
    if index == 0:
        return False

    # Check lines before for XML doc comments or attributes
    i = index - 1
    while i >= 0:
        line = lines[i].strip()
        if not line:
            i -= 1
            continue
        if line.startswith('///'):
            return True
        if line.startswith('['):  # Attribute
            i -= 1
            continue
        break
    return False

def add_docs_to_file(filepath):
    """Add missing XML documentation to a file."""
    with open(filepath, 'r', encoding='utf-8') as f:
        lines = f.readlines()

    new_lines = []
    modified = False
    i = 0

    while i < len(lines):
        line = lines[i]
        stripped = line.strip()

        # Check if this is a public member without documentation
        if stripped.startswith('public ') and not has_xml_doc_before(lines, i):
            indent = ' ' * get_indent(line)

            # Constructor
            if re.search(r'public\s+\w+\s*\(', stripped) and 'class ' not in stripped:
                class_match = None
                for j in range(i-1, max(0, i-20), -1):
                    class_match = re.search(r'(?:public|internal)\s+(?:sealed\s+)?class\s+(\w+)', lines[j])
                    if class_match:
                        break

                if class_match:
                    class_name = class_match.group(1)
                    constructor_match = re.search(r'public\s+(\w+)\s*\(', stripped)
                    if constructor_match and constructor_match.group(1) == class_name:
                        new_lines.append(f'{indent}/// <summary>\n')
                        new_lines.append(f'{indent}/// Initializes a new instance of the <see cref="{class_name}"/> class.\n')
                        new_lines.append(f'{indent}/// </summary>\n')
                        modified = True

            # Method
            elif 'Task' in stripped and '(' in stripped:
                method_match = re.search(r'public\s+(?:async\s+)?Task<?([^>]*?)>?\s+(\w+)', stripped)
                if method_match:
                    method_name = method_match.group(2) if method_match.lastindex >= 2 else method_match.group(1)
                    desc = f"Performs the {method_name} operation."
                    if method_name.startswith('Get'):
                        desc = f"Gets the requested data."
                    elif method_name.startswith('Set'):
                        desc = f"Sets the requested data."
                    elif method_name.startswith('Fetch'):
                        desc = f"Fetches the requested data."
                    elif method_name.startswith('Parse'):
                        desc = f"Parses the provided data."
                    elif method_name.startswith('Build'):
                        desc = f"Builds the requested object."
                    elif method_name.startswith('Refresh'):
                        desc = f"Refreshes the data."
                    elif method_name.startswith('Search'):
                        desc = f"Searches for the requested data."
                    elif method_name.startswith('Lookup'):
                        desc = f"Looks up the requested data."
                    elif method_name.startswith('Screen'):
                        desc = f"Screens for matching items."
                    elif method_name.startswith('Listen'):
                        desc = f"Listens for updates."
                    elif method_name.startswith('Repair'):
                        desc = f"Repairs the provided data."
                    elif method_name.startswith('Convert'):
                        desc = f"Converts the provided data."
                    elif method_name.startswith('Fix'):
                        desc = f"Fixes the provided data."

                    new_lines.append(f'{indent}/// <summary>\n')
                    new_lines.append(f'{indent}/// {desc}\n')
                    new_lines.append(f'{indent}/// </summary>\n')
                    modified = True

            # Property
            elif re.search(r'public\s+\w+(<[^>]+>)?\s+\w+\s*{\s*get', stripped):
                prop_match = re.search(r'public\s+\w+(?:<[^>]+>)?\s+(\w+)', stripped)
                if prop_match:
                    prop_name = prop_match.group(1)
                    has_set = 'set' in stripped
                    verb = "Gets or sets" if has_set else "Gets"
                    new_lines.append(f'{indent}/// <summary>\n')
                    new_lines.append(f'{indent}/// {verb} the {prop_name}.\n')
                    new_lines.append(f'{indent}/// </summary>\n')
                    modified = True

        new_lines.append(line)
        i += 1

    if modified:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.writelines(new_lines)
        return True
    return False

def main():
    """Main function to process all C# files."""
    base_path = Path('/home/runner/work/YFinance.NET/YFinance.NET')

    # Process Implementation directory
    impl_dir = base_path / 'YFinance.NET.Implementation'

    count = 0
    for cs_file in impl_dir.rglob('*.cs'):
        # Skip obj and bin directories
        if '/obj/' in str(cs_file) or '/bin/' in str(cs_file):
            continue

        if add_docs_to_file(cs_file):
            count += 1
            print(f'Fixed: {cs_file.name}')

    print(f'\nTotal files modified: {count}')

if __name__ == '__main__':
    main()
