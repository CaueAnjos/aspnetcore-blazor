{
  inputs.nixpkgs.url = "https://flakehub.com/f/NixOS/nixpkgs/0";
  inputs.flake-utils.url = "github:numtide/flake-utils";

  outputs = {
    nixpkgs,
    flake-utils,
    ...
  }:
    flake-utils.lib.eachDefaultSystem (system: let
      pkgs = nixpkgs.legacyPackages.${system};
      dotnet-sdk = with pkgs.dotnetCorePackages;
        combinePackages [
          sdk_10_0
          sdk_9_0
        ];
    in {
      devShells.default = pkgs.mkShellNoCC {
        packages = with pkgs; [
          dotnet-sdk
          dotnet-ef
          csharpier
          podman
          podman-compose
        ];
      };
    });
}
