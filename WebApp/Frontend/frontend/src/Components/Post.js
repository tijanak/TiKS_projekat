import React, { useState, useEffect } from "react";

export function Post(id_posta) {
  const [post, setPost] = useState(null);
  useEffect(() => {
    fetch(`http://localhost:${window.location.port}/Slucaj/Get/${id_posta}`, {
      method: "GET",
    })
      .then(response => response.json())
      .then(data => {
        console.log(data);
        setPost(data);
      })
      .catch(error => console.log(error));
  }, []);
  return (
    <>{
        (post&&
        <div>
            <h2>{post.Naziv}</h2>
            <p>{post.Opis}</p>
            {post.Slike.forEach(element => {
                <img src={element} alt="slika"></img>
            })};
            {post.Kategorija.forEach(element => {
                <h2>{element.Tip}</h2>
            })};
            <button>doniraj</button>
            <button>udomi</button>
            <button>novosti</button>
        </div>)
        ||<>ne ucitava post</>
    }</>
  );
}