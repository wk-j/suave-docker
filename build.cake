// Required in order to use the addin Cake.Paket because it downloads paket.exe.
#tool nuget:?package=Paket

// Required in order to use PaketRestore, PaketPack, and PaketPush.
#addin nuget:?package=Cake.Paket

Task("Restore").Does(() => {
    PaketRestore();
});

Action<string,string> start = (cmd, args) => {
    StartProcess(cmd, new ProcessSettings {
        Arguments = args
    });
};

Task("Build-Docker").Does(() => {
    start("docker", "build -t forki/suave:0.1 .");
});

Task("Run-Docker").Does(() => {
    start("docker", "run -d -p 8080:8080 forki/suave:0.1");
});

var target = Argument("target", "default");
RunTarget(target);