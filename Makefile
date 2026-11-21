.DEFAULT_GOAL := help

.PHONY: help dev-catalog build test start format clean restore

help:
	@echo "Available commands:"
	@echo "  make help        - Show available commands"
	@echo "  make dev-catalog - Start Catalog API with hot reload"
	@echo "  make build       - Build the solution"
	@echo "  make test        - Run all tests"
	@echo "  make start       - Start Catalog API without hot reload"
	@echo "  make format      - Format C# code using dotnet format"
	@echo "  make clean       - Remove build outputs"
	@echo "  make restore     - Restore NuGet dependencies"

dev-catalog:
	dotnet watch run --project src/Catalog.Api

build:
	dotnet build

test:
	dotnet test

start:
	dotnet run --project src/Catalog.Api

format:
	dotnet format

clean:
	dotnet clean

restore:
	dotnet restore
