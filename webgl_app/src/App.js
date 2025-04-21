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
      <div>
          <div className="title-center">
              <h2>Triangulation 3d Demo</h2>
          </div>
          <div className="unity-content">
              <Unity unityProvider={unityProvider}
                     style={{
                         height: "1080px",
                         width: "1920px",
                         border: "2px solid black",
                         background: "grey",
                     }}/>
          </div>
      </div>
  );
}

export default App;
