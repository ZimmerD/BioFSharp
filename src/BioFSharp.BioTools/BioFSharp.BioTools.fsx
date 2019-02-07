﻿#r "netstandard"
#r "../../packages/Newtonsoft.Json.10.0.3/lib/netstandard1.3/Newtonsoft.Json.dll"
#r "../../packages/System.Buffers/lib/netstandard2.0/System.Buffers.dll"
#r "../../packages/Docker.DotNet/lib/netstandard2.0/Docker.DotNet.dll"

#r "../../packages/SharpZipLib/lib/netstandard2.0/ICSharpCode.SharpZipLib.dll"

#r "../../packages/SharpZipLib/lib/netstandard2.0/ICSharpCode.SharpZipLib.dll"
#r "../../packages/FSharpAux.IO/lib/netstandard2.0/FSharpAux.dll"
#r "../../packages/FSharpAux.IO/lib/netstandard2.0/FSharpAux.IO.dll"

#load "Docker.fs"
#load "BioContainerIO.fs"
#load "BioContainer.fs"
#load "TargetP.fs"

open System.Threading
open System.Threading
open Docker.DotNet
open System.Threading
open System.Buffers
open System.Threading.Tasks

open BioFSharp.BioTools
open System.Collections.Generic
open Docker.DotNet.Models
open System.IO


open ICSharpCode.SharpZipLib.GZip
open ICSharpCode.SharpZipLib.Tar
open Newtonsoft.Json.Serialization
open System





let client = Docker.connect "npipe://./pipe/docker_engine"
let targetPimage = Docker.DockerId.ImageId "targetp"


let bcContext =
    BioContainer.initBcContextAsync client targetPimage
    |> Async.RunSynchronously


let stream = new FileStream("C:/tmp/myTest.fsa",FileMode.Open)

let res = TargetP.run bcContext (TargetP.NonPlant) stream



BioContainer.disposeAsync bcContext
|> Async.Start



//  " 
//### targetp v1.1 prediction results ##################################
//Number of query sequences:  1
//Cleavage site predictions not included.
//Using NON-PLANT networks.

//Name	Len	mTP	SP	other	Loc	RC
//----------------------------------------------------------------------
//P11111;             	1088	 0.054	 0.068	 0.943	_	1
//"







//// https://github.com/Microsoft/Docker.DotNet/issues/223 -> write
//// https://github.com/Microsoft/Docker.DotNet/issues/212 -> read

////-i, --interactive=false Keep STDIN open even if not attached
////-t, --tty=false Allocate a pseudo-TTY

//let exe = 
//    async {
//        //let! container =
//        //    let param = Docker.Container.ContainerParams.InitCreateContainerParameters(Image=dockerid.ToString(),Cmd=cmd,OpenStdin=true)
//        //    Docker.Container.createContainerWithAsync connection param      
        
//        //let! isRunning =
//        //    let param = 
//        //        Docker.Container.ContainerParams.InitContainerStartParameters()

//        //    Docker.Container.startContainerWithAsync connection param container.ID

//        let! execContainer =
//            let param = 
//                Docker.Container.ContainerParams.InitContainerExecCreateParameters(                                        
//                    AttachStderr=true,
//                    AttachStdout=true,                
//                    AttachStdin=false,
//                    Cmd=cmd',
//                    Detach=false
//                    //Tty=false
//                    )

//            Docker.Container.execCreateContainerAsync connection param (cont)
//        return tmp
//        }

//    |> Async.RunSynchronously


//////docker stop $(docker ps -a -q)

////Docker.Container.removeContainerAsync connection (Docker.DockerId.ContainerId (container.ID))  
////|> Async.RunSynchronously


//let ms = 
//    async {
        
//        let! execContainer =
//            let param = 
//                Docker.Container.ContainerParams.InitContainerExecCreateParameters(                                        
//                    AttachStderr=true,
//                    AttachStdout=true,                
//                    AttachStdin=false,
//                    Cmd=cmd,
//                    Detach=false                    
//                    )

//            Docker.Container.execCreateContainerAsync connection param (cont)

//        let! stream =
//            let param = 
//                Docker.Container.ContainerParams.InitContainerExecStartParameters(
//                    AttachStderr=true,
//                    AttachStdout=true,                
//                    AttachStdin=false,                   
//                    Cmd=cmd
//                    )                
//            Docker.Container.startContainerWithExecConfigAsync connection param cont // startContainerExecAsync connection exe.ID // 
            
//        printfn "Start Exec"
        
//        //let stopParam = new ContainerStopParameters()        
//        //let! st =  connection.Containers.StopContainerAsync(cont,stopParam) |> Async.AwaitTask
        
//        //printfn "Stop: %b" st
        
//        let stdOutputStream = new System.IO.MemoryStream()
//        let streamTask =
//            stream.CopyOutputToAsync(null,stdOutputStream,null,CancellationToken.None)             

                
//        do! streamTask |> Async.AwaitTask

//        printfn "Streamed"

//        //let! wait = 
//        //    Docker.Container.waitContainerAsync connection container.ID

//        let result =        
//            stdOutputStream.Position <- 0L
//            readFrom stdOutputStream
                    
//        //do! Docker.Container.removeContainerAsync connection (Docker.DockerId.ContainerId container.ID)  
    
//        return result
    
//    } 
//    |> Async.RunSynchronously



//let ms = 
//    async {
//        let! container =
//            let param = 
//                Docker.Container.ContainerParams.InitCreateContainerParameters(
//                    ArgsEscaped=false,
//                    AttachStderr=true,
//                    AttachStdout=true,                
//                    AttachStdin=false,
//                    Image=string dockerid,
//                    Cmd=cmd
//                    )

//            Docker.Container.createContainerWithAsync connection param              

//        //let! isRunning =
//        //    let param = Docker.Container.ContainerParams.InitContainerStartParameters()
//        //    Docker.Container.startContainerWithAsync connection param container.ID

//        let! stream = 
//            let param = Docker.Container.ContainerParams.InitContainerAttachParameters (Stdout=true,Stderr=true,Stdin=false,Stream=true)
//            connection.Containers.AttachContainerAsync(container.ID,false,param)
//            |> Async.AwaitTask
    
//        let stdOutputStream = new System.IO.MemoryStream()
//        let streamTask =
//            stream.CopyOutputToAsync(null,stdOutputStream,null,CancellationToken.None) 

//        let! isRunning =
//            let param = 
//                Docker.Container.ContainerParams.InitContainerExecStartParameters(
//                    AttachStderr=true,
//                    AttachStdout=true,                
//                    AttachStdin=false,                   
//                    Cmd=cmd
//                    )                
//            Docker.Container.startContainerWithExecConfigAsync connection param container.ID
                
//        do! streamTask |> Async.AwaitTask

//        let! wait = 
//            Docker.Container.waitContainerAsync connection container.ID

//        let result =        
//            stdOutputStream.Position <- 0L
//            readFrom stdOutputStream
                    
//        do! Docker.Container.removeContainerAsync connection (Docker.DockerId.ContainerId container.ID)  
    
//        return result
    
//    } 
//    |> Async.RunSynchronously








































////let tmp =
////    BioContainer.runCmdAsync client (Docker.DockerId.ImageName "ubuntu") ["echo"; "hello world"]
////    |> Async.RunSynchronously
////    |> readFrom



//Docker.Image.exists client (Docker.DockerId.ImageName "targetp_image")


//Docker.Image.listImages client
//|> Seq.map (fun i -> i.ID )
//|> Seq.toArray


//Docker.Container.existsByImage client (Docker.DockerId.ImageName "targetp_image")


////ancestor=(<image-name>[:<tag>], <image id> or <image@digest>)

//let filters = 
//    Docker.Container.ContainerParams.InitContainerListParameters(All=true,Filters=Docker.Filters.InitContainerFilters(Ancestor=Docker.DockerId.ImageName "ubuntu"))


//Docker.Container.listContainersWithAsync client filters
//|> Async.RunSynchronously
//|> Seq.map (fun x -> x.Command,x.Image,x.Labels)
//|> Seq.toArray

////client.Containers.StartWithConfigContainerExecAsync
//let p = Docker.DotNet.Models.ContainerExecStartParameters()


//let ap = Docker.DotNet.Models.ContainerAttachParameters()




//Docker.Container.existsByImage client (Docker.DockerId.ImageName "targetp_image")


//let idtp = "61fbfbc30382e83dd585c99583c036ef8c5ced4eb10e1b274f199da6b6969588"

////let pipe = System.Uri("npipe://./pipe/docker_engine")

////let config = new DockerClientConfiguration(pipe)
////let client = config.CreateClient()

////let createByImage (client:DockerClient) imageName =
////    async {
////        let param = Models.CreateContainerParameters()
////        param.Image <- imageName
////        param.Cmd <- System.Collections.Generic.List(["echo"; "hello world"])
////        let! container =  
////            client.Containers.CreateContainerAsync (param,CancellationToken.None)
////            |> Async.AwaitTask
////        return container.ID
////    }


////let result =
////    async {
////        let paramLog = Models.ContainerLogsParameters() // (Stdout = System.Nullable<bool>(true),Stdin = System.Nullable<bool>(true))
////        paramLog.ShowStdout <- System.Nullable<bool>(true)
////        let paramRun = Models.ContainerStartParameters ()
        
////        //let id = 
////        //    "4243adc7f3832ea35bdaad79aabe86f8e1c54f5c3a799cc72e060a8402bc24cb"
        
////        let! id = createByImage client "ubuntu"

////        let! isRunnig =  
////            client.Containers.StartContainerAsync(id,paramRun,CancellationToken.None)
////            |> Async.AwaitTask
        
////        let! wait = 
////            client.Containers.WaitContainerAsync(id,CancellationToken.None)
////            |> Async.AwaitTask
        
////        let! logs =
////            client.Containers.GetContainerLogsAsync (id,paramLog,CancellationToken.None)
////            |> Async.AwaitTask

            
        
////        return logs
////    } 
////    |> Async.RunSynchronously


////let tmp : array<byte> = Array.zeroCreate 1024
////result.Read(tmp,0,1024)

////System.Text.Encoding.UTF8.GetString(tmp,0,1024)

