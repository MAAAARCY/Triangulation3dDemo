import './App.css';

import React from "react";
import { Unity, useUnityContext } from "react-unity-webgl";

function App() {
  const { unityProvider } = useUnityContext({
    loaderUrl: "UnityBuild/Build/UnityBuild.loader.js",
    dataUrl: "UnityBuild/Build/UnityBuild.data",
    frameworkUrl: "UnityBuild/Build/UnityBuild.framework.js",
    codeUrl: "UnityBuild/Build/UnityBuild.wasm", 
    streamingAssetsUrl: "UnityBuild/StreamingAssets"
  });

  return (
      <div className="Unity-Content">
        <Unity unityProvider={unityProvider}
               style={{
                 height: "1080px",
                 width: "1920px",
                 border: "2px solid black",
                 background: "grey",
               }}/>
      </div>
  );
}

export default App;
