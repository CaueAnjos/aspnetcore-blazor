#!/usr/bin/env bash

echo "Starting db with podman-compose"
podman-compose up -d
sleep 5

echo "Running mirgrations"
MIGRATIONS="./src/ScreenSound.API"
dotnet ef database update --project $MIGRATIONS

echo "Running API"
PROJECT="./src/ScreenSound.API"
dotnet run --project $PROJECT
