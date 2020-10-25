<template>

    <div  class="container-fluid">
        <div class="row my-5">

            <div class="col-12">
                <div class="row">
                    <div class="col-sm-12 col-md-2">
                        <img :src="viewedProfile.picture" alt="">
                    </div>
                    <div class="col-sm-12 col-md-10">
                        <h2>{{viewedProfile.name}}</h2>
                        <h5>Public Vaults: {{vaults.length}}</h5>
                        <h5>Keeps: {{keeps.length}}</h5>
                    </div>
                </div>
            </div>

        </div>
        <div class="row my-1">
            <div class="col-12">
                <h3>Keeps <i class="fas fa-plus text-success"></i></h3>
            </div>
        </div>
        <div class="row">

            <keep-component v-for="keep in keeps" :key="keep.id" :keepProp="keep"/>

        </div>
    </div>
  
</template>

<script>
import keepComponent from "../components/KeepComponent"
export default {
    mounted(){
        this.$store.dispatch("getSearchedProfile", this.$route.params.id)
        this.$store.dispatch("getProfileKeeps", this.$route.params.id);
        this.$store.dispatch("getProfileVaults", this.$route.params.id);
    },
    name: "profile",
    computed: {
        keeps(){
            return this.$store.state.profileKeeps;
        },
        vaults(){
            return this.$store.state.profileVaults;
        },
        viewedProfile(){
            return this.$store.state.viewedProfile
        }
    },
    components:{
        keepComponent
    }


}
</script>

<style>

</style>