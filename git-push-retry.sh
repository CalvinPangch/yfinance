#!/bin/bash
set -e

BRANCH="${1:-$(git branch --show-current)}"
MAX_RETRIES=4
RETRY_COUNT=0

echo "Pushing to branch: $BRANCH"

while [ $RETRY_COUNT -lt $MAX_RETRIES ]; do
  if git push -u origin "$BRANCH"; then
    echo "✅ Push successful!"
    exit 0
  else
    RETRY_COUNT=$((RETRY_COUNT + 1))
    if [ $RETRY_COUNT -lt $MAX_RETRIES ]; then
      DELAY=$((2 ** RETRY_COUNT))  # Exponential backoff: 2s, 4s, 8s, 16s
      echo "⚠️ Push failed. Retrying in ${DELAY}s... (Attempt $((RETRY_COUNT + 1))/$MAX_RETRIES)"
      sleep $DELAY
    else
      echo "❌ Push failed after $MAX_RETRIES attempts"
      exit 1
    fi
  fi
done
