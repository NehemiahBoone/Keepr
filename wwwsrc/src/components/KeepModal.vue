<template>
  <div
    class="modal fade"
    :id="id"
    data-backdrop="static"
    tabindex="-1"
    role="dialog"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <slot name="header"></slot>
          <button type="button" class="close" data-dismiss="modal">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <slot name="body"></slot>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "KeepModal",
  props: {
    id: {
      type: String,
      required: true,
    },
  },
};
</script>

<style>
</style>