import React, { useState, useEffect } from "react";
import BACKEND from "../config";
import { useLocation } from "react-router-dom";
import { Novosti } from "./Novost";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
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
        <Card key={post.id} sx={{ maxWidth: 345 }}>
        <CardActions>
          <Button size="small">Doniraj</Button>

          <Button size="small">Udomi</Button>
        </CardActions>
        {/* <CardMedia
          sx={{ height: 140 }}
          image="https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.istockphoto.com%2Fphotos%2Fplaceholder-image&psig=AOvVaw3iu14a8dxZufPTObHcDmUR&ust=1705892562250000&source=images&cd=vfe&ved=0CBMQjRxqFwoTCKDv5ZO_7YMDFQAAAAAdAAAAABAE"
          title="slika"
        /> */}
        <CardContent>
          <Typography gutterBottom variant="h5" component="div">
            {post.naziv}
          </Typography>
          <Typography variant="body2" color="text.secondary">
            {post.opis}
          </Typography>
        </CardContent>
        <Typography variant="subtitle2" component="div">
            NOVOSTI
          </Typography>
        { post && <Novosti novost={post.id}></Novosti>}
      </Card>
      )) || <>ne ucitava post</>}
    </>
  );
}
