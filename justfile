api_dir := "." / "Api"
frontend_dir := ".." / "PersuadeMate_frontend"
bun_installed_file := frontend_dir / "node_modules" / ".installed_timestamp"
bun_package_json := frontend_dir / "package.json"

# just のみで実行されるタスク 'watch'
default: watch    

# すべてのタスクを一覧する
tasks:
    @just --list

# フロントエンドアプリとバックエンドAPIを同時に実行する
watch:
    #!/usr/bin/env bash
    commands=(
        "just run_api watch"
        "just run_fe"
    )
    printf "%s\n"  "${commands[@]}" | xargs -I {} -P 2 sh -c "{}"

# バックエンドAPIを実行する(run: 単純に実行(既定)、watch: ファイルを監視して実行)
run_api run_or_watch='run': (check_command "dotnet")
    @if test {{ run_or_watch }} = 'watch'; then \
        dotnet watch --project {{ api_dir }}; \
    else \
        dotnet run --project {{ api_dir }}; \
    fi

# フロントエンドアプリを実行する
run_fe: (check_command "bun") bun_install
    cd {{ frontend_dir }} && bun run dev

# package.json ファイルが更新された場合に bun install を実行する (force: 強制的に bun install する)
bun_install force='none': (check_command "bun") (check_command "stat")
    #!/usr/bin/env bash
    cd {{ frontend_dir }}
    if test "{{ force }}" = "force"; then
        bun install
    else
        package_json_timestamp=$(stat -c "%Y" {{ bun_package_json }})
        if test -f {{ bun_installed_file }}; then
            bun_installed_timestamp=$(stat -c "%Y" {{ bun_installed_file }})
            if test $package_json_timestamp -gt $bun_installed_timestamp; then
                bun install
            fi
        else
            bun install
        fi
    fi
    touch {{ bun_installed_file }}

# 端末に指定したコマンドが存在するかを確認する
check_command cmd:
    @if ! command -v {{cmd}} &> /dev/null; then \
        echo "{{cmd}} is not found" && exit 1; \
    fi

