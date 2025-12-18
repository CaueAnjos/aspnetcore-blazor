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

        shellHook = let
          podmanSetupScript = let
            registriesConf = pkgs.writeText "registries.conf" ''
              [registries.search]
              registries = ['docker.io']

              [registries.block]
              registries = []
            '';
          in
            pkgs.writeScript "podman-setup" ''
              #!${pkgs.runtimeShell}

              # Dont overwrite customised configuration
              if ! test -f ~/.config/containers/policy.json; then
                install -Dm555 ${pkgs.skopeo.src}/default-policy.json ~/.config/containers/policy.json
              fi

              if ! test -f ~/.config/containers/registries.conf; then
                install -Dm555 ${registriesConf} ~/.config/containers/registries.conf
              fi
            '';
        in ''
          Installing required configurations for podman
          ${podmanSetupScript}
        '';
      };
    });
}
