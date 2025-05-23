
# Define the URL of the local host
$url = "https://localhost:7238/Cache/Add"

# Function to generate a random string
function Get-RandomString($length) {
    $chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
    $string = -join ((1..$length) | ForEach-Object { $chars[(Get-Random -Maximum $chars.Length)] })
    return $string
}

# Loop to send 1000 POST requests
for ($i = 1; $i -le 1000; $i++) {
    # Randomly decide if the key will be valid or invalid
    $isValid = Get-Random -Maximum 2
    if ($isValid -eq 1) {
        $key = Get-RandomString -length 8  # Valid key
    } else {
        $key = Get-RandomString -length (Get-Random -Minimum 1 -Maximum 7)  # Invalid key
    }
    $value = Get-RandomString -length 30
    $body = @{ key = $key; value = $value } | ConvertTo-Json
    Invoke-RestMethod -Uri $url -Method Post -Body $body -ContentType "application/json"
    Write-Host "Request $i sent with key: $key and value: $value (Valid: $($isValid -eq 1))"
}
