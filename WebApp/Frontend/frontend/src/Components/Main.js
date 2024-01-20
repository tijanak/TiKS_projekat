import React, { useState, useEffect } from "react";
import BACKEND from "../config";
function Main() {
  const [posts, setPosts] = useState(null);
  useEffect(() => {
    fetch(`${BACKEND}Slucaj/Get/All`, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setPosts(data);
      })
      .catch((error) => console.log(error));
  }, []);
  return (
    <div>
      <h2>nesto iz baze:</h2>
      {posts && <p>{posts}</p>}
    </div>
  );
}
export default Main;
