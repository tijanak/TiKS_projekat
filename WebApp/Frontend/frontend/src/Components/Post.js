import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import { useLocation } from "react-router-dom";
export function Post() {
  const [post, setPost] = useState(null);

  const location = useLocation();
  useEffect(() => {
    console.log(location);
    setPost(location.state.post);
  }, []);
  return (
    <>
      {(post && (
        <div>
          <h2>{post.naziv}</h2>
          <p>{post.ppis}</p>
          {post.slike.forEach((element) => {
            <img src={element} alt="slika"></img>;
          })}
          ; ;<button>doniraj</button>
          <button>udomi</button>
          <button>novosti</button>
        </div>
      )) || <>ne ucitava post</>}
    </>
  );
}
