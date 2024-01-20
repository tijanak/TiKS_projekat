import React, { useState, useEffect } from "react";
function Main() {
  const [posts, setPosts] = useState(null);
  useEffect(() => {
    fetch("http://localhost:5100/Slucaj/Get/All", {
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
